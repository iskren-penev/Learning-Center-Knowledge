namespace LearningCenter.App.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.ViewModels.User;
    using LearningCenter.Services.Interfaces;
    using Microsoft.AspNet.Identity;

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
        [Route("profile/{username}")]
        public ActionResult ProfilePage(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileViewModel viewModel = this.service.GetProfileViewModel(username);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("edit/{username}")]
        public ActionResult Edit(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!User.Identity.Name.StartsWith(username))
            {
                return this.RedirectToAction("ProfilePage", new {username = username.ToLower()});
            }
            EditProfileViewModel viewModel = this.service.GetEditProfileViewModel(username);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
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
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }
        
        }
}