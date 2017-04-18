namespace LearningCenter.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using LearningCenter.Models.ViewModels.Admin;
    using LearningCenter.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    [Authorize(Roles = "Admin")]
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        private IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("users")]
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
        [Route("addCourse")]
        public ActionResult AddCourse()
        {
            return this.View();
        }


    }
}
