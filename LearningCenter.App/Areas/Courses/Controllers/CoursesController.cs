namespace LearningCenter.App.Areas.Courses.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.Courses;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Quiz;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Interfaces;
    using Microsoft.AspNet.Identity;


    [Authorize(Roles = "Admin,Instructor,User")]
    [RouteArea("courses")]
    public class CoursesController : Controller
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
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = string.Empty;
            if (this.User != null)
            {
                userId = this.User.Identity.GetUserId();
            }
            DetailedCourseViewModel viewModel = this.service.GetDetailedCourseViewModel(id, userId);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("showunitdescription")]
        public ActionResult ShowDescription(string Description)
        {
            if (string.IsNullOrEmpty(Description))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return this.PartialView("_ShowDescription", Description);
        }

        [HttpGet]
        [Route("showunitcontent")]
        public ActionResult ShowUnitContent(int unitId)
        {
            if (unitId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitDetailsViewModel viewModel = this.service.GetUnitPreview(unitId);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }
            return this.PartialView("_ShowUnitContent", viewModel);
        }

        [HttpGet]
        [Route("showquiz")]
        public ActionResult ShowQuiz(int quizId)
        {
            if (quizId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreviewQuizViewModel viewModel = this.service.GetQuizPreview(quizId);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }
            return this.PartialView("_ShowQuiz", viewModel);
        }

        [HttpPost]
        [Route("showquiz")]
        public ActionResult ShowQuiz(
            [Bind(Include =
                "Id,AnswerOne,AnswerTwo,AnswerThree,AnswerFour,AnswerFive,AnswerSix,AnswerSeven,AnswerEight,AnswerNine,AnswerTen")]
            EvaluateQuizBindingModel model)
        {
            string userId = this.User.Identity.GetUserId();
            int gradeId = this.service.EvaluateQuiz(model, userId);
            if (gradeId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.RedirectToAction("QuizResult", new { id = gradeId });
        }

        [HttpGet]
        [Route("grade/{id:int:min(1)}")]
        public ActionResult QuizResult(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeViewModel viewModel = this.service.GetGradeViewModel(id);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }
            return this.View(viewModel);
        }

        [Route("enrollincourse")]
        public ActionResult EnrollInCourse(int courseId)
        {
            if (courseId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            this.service.EnrollUser(userId, courseId);
            return this.RedirectToAction("Detailed", new { id = courseId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        [Route("add")]
        public ActionResult AddCourse()
        {
            return this.View(new AddCourseViewModel());
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        [Route("add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse([Bind(Include = "Title,ShortDescription,Description")]AddCourseBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddCourse(model);
                return this.RedirectToAction("CourseList", "Admin", new { area = "Admin" });
            }

            AddCourseViewModel viewModel = this.service.GetAddCourseViewModel(model);
            
            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        [Route("edit/{id:int:min(1)}")]
        public ActionResult EditCourse(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditCourseViewModel viewModel = this.service.GetEditCourseViewModel(id);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
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
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [Authorize(Roles = "Admin,Instructor")]
        [Route("addcourseunit")]
        public ActionResult AddCourseUnit(int unitId, int courseId)
        {
            if (unitId <= 0 || courseId <=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            this.service.AddUnitToCourse(unitId, courseId);

            return this.RedirectToAction("EditCourse", new { id = courseId });
        }

        [Authorize(Roles = "Admin,Instructor")]
        [Route("removecourseunit")]
        public ActionResult RemoveCourseUnit(int unitId, int courseId)
        {
            if (unitId <= 0 || courseId <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            this.service.RemoveUnitFromCourse(unitId, courseId);

            return this.RedirectToAction("EditCourse", new { id = courseId });
        }
    }
}