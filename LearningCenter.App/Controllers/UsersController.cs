namespace LearningCenter.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.ViewModels.User;
    using LearningCenter.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    [Authorize]
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route]
        public ActionResult All(string search)
        {
            IEnumerable<AllUserViewModel> viewModels = this.service.GetAllUsers(search);
            var accManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            foreach (var model in viewModels)
            {
                var user = this.service.GetCurrentUserByEmail(model.Email);
                var roles = accManager.GetRoles(user.Id).ToList();
                this.service.SetRoleNameForModel(model, roles);
            }

            return this.View(viewModels);
        }

        [HttpGet]
        [Route("profile/{username}")]
        public ActionResult ProfilePage(string username)
        {
            ProfileViewModel viewModel = this.service.GetProfileViewModel(username);
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("edit/{username}")]
        public ActionResult Edit(string username)
        {
            if (!User.Identity.Name.StartsWith(username))
            {
                return this.RedirectToAction("ProfilePage", new {username = username.ToLower()});
            }
            EditProfileViewModel viewModel = this.service.GetEditProfileViewModel(username);
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("edit/{username}")]
        public ActionResult Edit([Bind(Include = "FirstName,LastName")]EditProfileBindingModel model)
        {
            string userId = User.Identity.GetUserId();

            var indexOfAt = User.Identity.Name.IndexOf("@");
            var currentUsername = User.Identity.Name.Substring(0, indexOfAt);

            if (this.ModelState.IsValid)
            {
                this.service.EditProfile(model, userId);
                return this.RedirectToAction("ProfilePage", new {username = currentUsername});
            }
            EditProfileViewModel viewModel = this.service.GetEditProfileViewModel(currentUsername);
            return this.View(viewModel);
        }
    }
}