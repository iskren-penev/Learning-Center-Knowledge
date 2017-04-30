namespace LearningCenter.App.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Account;
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

        public AdminController(ApplicationUserManager userManager)
        {
            Manager = userManager;
        }

        private ApplicationUserManager Manager
        {
            get { return this.accManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            set { this.accManager = value; }
        }

        [HttpGet]
        [Route("users")]
        public ActionResult UsersList()
        {
            IEnumerable<UserListViewModel> viewModels = this.service.GetAllUsers();

            foreach (var model in viewModels)
            {
                var user = this.service.GetCurrentUserByEmail(model.Email);
                var roles = this.Manager.GetRoles(user.Id).ToList();
                this.service.SetRoleNameForModel(model, roles);
            }
            return this.View(viewModels);
        }
        

        public PartialViewResult SearchUsers(string search)
        {
            IEnumerable<UserListViewModel> viewModels = this.service.SearchUsers(search);

            foreach (var model in viewModels)
            {
                var user = this.service.GetCurrentUserByEmail(model.Email);
                var roles = this.Manager.GetRoles(user.Id).ToList();
                this.service.SetRoleNameForModel(model, roles);
            }
            return this.PartialView("_SearchUsers", viewModels);
        }

        
        [Route("users/add")]
        public ActionResult AddUser()
        {
            return View();
        }
        
        [HttpPost]
        [Route("users/add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser([Bind(Include = "Email,Password,FirstName,LastName,ConfirmPassword")]RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await Manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   this.Manager.AddToRole(user.Id, "User");
                    
                    return this.RedirectToAction("UsersList");
                }
                this.AddErrors(result);
            }
            
            return this.View(model);
        }

        [HttpPost]
        public ActionResult MakeInstructor(string userId)
        {
            if (this.service.CheckUserExists(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User with this Id does not exist");
            }
            this.Manager.AddToRole(userId, "Instructor");
            return this.RedirectToAction("UsersList");

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        [Route("courses")]
        public ActionResult CourseList()
        {
            IEnumerable<CourseListViewModel> viewModels = this.service.GetAllCourses();
            return this.View(viewModels);
        }

        [HttpGet]
        public PartialViewResult SearchCourses(string search)
        {
            IEnumerable<CourseListViewModel> viewModels = this.service.SearchCourses(search);
            return this.PartialView("_SearchCourses", viewModels);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        [Route("units")]
        public ActionResult UnitsList()
        {
            IEnumerable<UnitListViewModel> viewModels = this.service.GetAllUnits();
            return this.View(viewModels);
        }

        [HttpGet]
        public PartialViewResult SearchUnits(string search)
        {
            IEnumerable<UnitListViewModel> viewModels = this.service.SearchUnits(search);
            return this.PartialView("_SearchUnits", viewModels);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}
