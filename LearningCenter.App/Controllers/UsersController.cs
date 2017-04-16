
namespace LearningCenter.App.Controllers
{
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
        [Route("profile")]
        public ActionResult ProfilePage()
        {
            string userId = User.Identity.GetUserId();
            ProfileViewModel viewModel = this.service.GetProfileViewModel(userId);
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("edit")]
        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            EditProfileViewModel viewModel = this.service.GetEditProfileViewModel(userId);
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("edit")]
        public ActionResult Edit([Bind(Include = "FirstName,LastName")]EditProfileBindingModel model)
        {
            string userId = User.Identity.GetUserId();

            if (this.ModelState.IsValid)
            {
                this.service.EditProfile(model, userId);
                return this.RedirectToAction("ProfilePage");
            }
            EditProfileViewModel viewModel = this.service.GetEditProfileViewModel(userId);
            return this.View(viewModel);
        }
    }
}