namespace LearningCenter.App.Areas.Courses.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.Courses;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Interfaces;
    using Microsoft.AspNet.Identity;


    [Authorize(Roles = "Admin,User")]
    [RouteArea("courses")]
    public class CoursesController: Controller
    {
        private ICourseService service;

        public CoursesController(ICourseService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route]
        [AllowAnonymous]
        public ActionResult AllCourses()
        {
            IEnumerable<AllCourseViewModel> courses = this.service.GetAllCourses();

            return this.View(courses);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [AllowAnonymous]
        public ActionResult Detailed(int id)
        {
            string userId = this.User.Identity.GetUserId();
            DetailedCourseViewModel viewModel = this.service.GetDetailedCourseViewModel(id, userId);
            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("showunitdescription")]
        public PartialViewResult ShowDescription(string Description)
        {
            return this.PartialView("_ShowDescription",Description);
        }

        [HttpGet]
        [Route("showunitcontent")]
        public PartialViewResult ShowUnitContent(int unitId)
        {
            UnitDetailsViewModel viewModel = this.service.GetUnitPreview(unitId);
            return this.PartialView("_ShowUnitContent", viewModel);
        }

        [Route("enrollincourse")]
        public RedirectToRouteResult EnrollInCourse(int courseId)
        {
            string userId = User.Identity.GetUserId();
            this.service.EnrollUser(userId, courseId);
            return this.RedirectToAction("Detailed", new {id = courseId});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("add")]
        public ActionResult AddCourse()
        {
            return this.View(new AddCourseViewModel());
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse([Bind(Include = "Title,ShortDescription,Description")]AddCourseBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddCourse(model);
                return this.RedirectToAction("CourseList", "Admin", new {area = "Admin"});
            }

            AddCourseViewModel viewModel = this.service.GetAddCourseViewModel(model);
            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("edit/{id:int:min(1)}")]
        public ActionResult EditCourse(int id)
        {
            EditCourseViewModel viewModel = this.service.GetEditCourseViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("edit/{id:int:min(1)}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse([Bind(Include = "Id,Title,ShortDescription,Description")] EditCourseBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditCourse(model);

                return this.RedirectToAction("Detailed", "Courses", new { area = "Courses", id = model.Id });
            }

            EditCourseViewModel viewModel = this.service.GetEditCourseViewModel(model.Id);
            return this.View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [Route("addcourseunit")]
        public RedirectToRouteResult AddCourseUnit(int unitId, int courseId)
        {
            this.service.AddUnitToCourse(unitId, courseId);

            return this.RedirectToAction("EditCourse", new { id = courseId });
        }

        [Authorize(Roles = "Admin")]
        [Route("removecourseunit")]
        public RedirectToRouteResult RemoveCourseUnit(int unitId, int courseId)
        {
            this.service.RemoveUnitFromCourse(unitId, courseId);

            return this.RedirectToAction("EditCourse", new { id = courseId });
        }
    }
}