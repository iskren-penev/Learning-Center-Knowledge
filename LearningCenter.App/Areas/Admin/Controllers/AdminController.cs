namespace LearningCenter.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.Admin;
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

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SearchUsers(string search)
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


        [AllowAnonymous]
        [Route("register")]
        public ActionResult AddUser()
        {
            return View();
        }
        
        [HttpPost]
        [Route("register")]
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

        [HttpGet]
        [Route("courses")]
        public ActionResult CourseList()
        {
            IEnumerable<CourseListViewModel> viewModels = this.service.GetAllCourses();
            return this.View(viewModels);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SearchCourses(string search)
        {
            IEnumerable<CourseListViewModel> viewModels = this.service.SearchCourses(search);
            return this.PartialView("_SearchCourses", viewModels);
        }

        [HttpGet]
        [Route("courses/add")]
        public ActionResult AddCourse()
        {
            return this.View(new AddCourseViewModel());
        }


        [HttpPost]
        [Route("courses/add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse([Bind(Include = "Title,ShortDescription,Description")]AddCourseBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddCourse(model);
                return this.RedirectToAction("CourseList");
            }

            AddCourseViewModel viewModel = this.service.GetAddCourseViewModel(model);
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("courses/edit/{id:int:min(1)}")]
        public ActionResult EditCourse(int id)
        {
            EditCourseViewModel viewModel = this.service.GetEditCourseViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("courses/edit/{id:int:min(1)}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse([Bind(Include = "Id,Title,ShortDescription,Description,UnitIds")] EditCourseBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditCourse(model);

                return this.RedirectToAction("Details", "Courses", new { area = "", id = model.Id });
            }

            EditCourseViewModel viewModel = this.service.GetEditCourseViewModel(model.Id);
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("units")]
        public ActionResult UnitsList()
        {
            IEnumerable<UnitListViewModel> viewModels = this.service.GetAllUnits();
            return this.View(viewModels);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SearchUnits(string search)
        {
            IEnumerable<UnitListViewModel> viewModels = this.service.SearchUnits(search);
            return this.PartialView("_SearchUnits", viewModels);
        }

        [HttpGet]
        [Route("units/{id:int:min(1)}")]
        public ActionResult UnitPreview(int id)
        {
            UnitDetailsViewModel viewModel = this.service.GetUnitDetailsViewModel(id);

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("units/add")]
        public ActionResult AddUnit()
        {
            return this.View(new AddUnitBindingModel());
        }

        [HttpPost]
        [Route("units/add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddUnit([Bind(Include = "Title,ContentUrl")]AddUnitBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddUnit(model);
                return this.RedirectToAction("UnitsList");
            }

            AddUnitViewModel viewModel = this.service.GetAddUnitViewModel(model);
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("units/edit/{id:int:min(1)}")]
        public ActionResult EditUnit(int id)
        {
            EditUnitViewModel viewModel = this.service.GetEditUnitViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("units/edit/{id:int:min(1)}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditUnit([Bind(Include = "")] EditUnitBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditUnit(model);
                return this.RedirectToAction("UnitPreview", new { id = model.Id });
            }

            EditUnitViewModel viewModel = this.service.GetEditUnitViewModel(model.Id);
            return this.View(viewModel);
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
