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
    [RouteArea("admin")]
    public class AdminController : Controller
    {
        private IAdminService service;
        private ApplicationUserManager accManager;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        private ApplicationUserManager Manager
        {
            get
            {
                return this.accManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [HttpGet]
        [Route("users")]
        public ActionResult Users()
        {
            IEnumerable<AllUserViewModel> viewModels = this.service.GetAllUsers();
            
            foreach (var model in viewModels)
            {
                var user = this.service.GetCurrentUserByEmail(model.Email);
                var roles = this.Manager.GetRoles(user.Id).ToList();
                this.service.SetRoleNameForModel(model, roles);
            }
            return this.View(viewModels);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Search(string search)
        {
            IEnumerable<AllUserViewModel> viewModels = this.service.SearchUsers(search);

            foreach (var model in viewModels)
            {
                var user = this.service.GetCurrentUserByEmail(model.Email);
                var roles = this.Manager.GetRoles(user.Id).ToList();
                this.service.SetRoleNameForModel(model, roles);
            }
            return this.PartialView("_SearchUsers",viewModels);
        }

        [HttpGet]
        [Route("addCourse")]
        public ActionResult AddCourse()
        {
            return this.View();
        }


    }
}
