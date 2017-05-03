namespace LearningCenter.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using LearningCenter.App;
    using LearningCenter.App.Areas.Admin.Controllers;
    using LearningCenter.App.Areas.Courses.Controllers;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Data.Mocks;
    using LearningCenter.Models.BindingModels.Courses;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Quiz;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Implementations;
    using LearningCenter.Services.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class TestCoursesController
    {
        private CoursesController controller;
        private ICourseService service;
        private ILearningCenterContext context;

        [TestInitialize]
        public void Initialize()
        {
            AutomapperConfig.RegisterMappings();
            this.context = new FakeLearningCenterContext();
            this.service = new CourseService(this.context);
            this.controller = new CoursesController(this.service);

            Course course = new Course()
            {
                Id = 1,
                Description = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                ShortDescription = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                Title = "TestCourse"
            };
            Unit unit = new Unit()
            {
                Id = 1,
                Title = "Test",
                ContentUrl = "someContent"
            };

            Quiz quiz = new Quiz()
            {
                Course = course,
                Id = 1, Title = "TestQuiz"
            };
            Grade grade = new Grade()
            {
                Id = 1, Course = course, CourseId = 1, QuizTitle = "QuizTitle", Result = 5
            };
            this.context.Courses.Add(course);
            this.context.Units.Add(unit);
            this.context.Quizzes.Add(quiz);
            this.context.Grades.Add(grade);
        }

        [TestMethod]
        public void AllCourses_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.AllCourses())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<AllCourseViewModel>>();
        }

        [TestMethod]
        public void Detailed_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.Detailed(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void Detailed_InvalidId_NotFound()
        {
            this.controller.WithCallTo(c => c.Detailed(int.MaxValue))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void Detailed_Valid_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.Detailed(1))
                .ShouldRenderDefaultView()
                .WithModel<DetailedCourseViewModel>();
        }

        [TestMethod]
        public void ShowDescription_NullParameter_BadRequest()
        {
            this.controller.WithCallTo(c => c.ShowDescription(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShowDescription_RenderPartialViewWithModel()
        {
            this.controller.WithCallTo(c => c.ShowDescription("some text here"))
                .ShouldRenderPartialView("_ShowDescription")
                .WithModel<string>();
        }

        [TestMethod]
        public void ShowUnitContent_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.ShowUnitContent(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShowUnitContent_InvalidId_NotFound()
        {
            this.controller.WithCallTo(c => c.ShowUnitContent(int.MaxValue))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShowUnitContent_Valid_RenderPartialViewWithModel()
        {
            this.controller.WithCallTo(c => c.ShowUnitContent(1))
                .ShouldRenderPartialView("_ShowUnitContent")
                .WithModel<UnitDetailsViewModel>();
        }

        [TestMethod]
        public void ShowQuiz_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.ShowQuiz(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShowQuiz_InvalidId_NotFound()
        {
            this.controller.WithCallTo(c => c.ShowQuiz(int.MaxValue))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShowQuiz_Valid_RenderPartialViewWithModel()
        {
            this.controller.WithCallTo(c => c.ShowQuiz(1))
                .ShouldRenderPartialView("_ShowQuiz")
                .WithModel<PreviewQuizViewModel>();
        }

        [TestMethod]
        public void QuizResult_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.QuizResult(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void QuizResult_InvalidId_NotFound()
        {
            this.controller.WithCallTo(c => c.QuizResult(int.MaxValue))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void QuizResult_Valid_RenderViewWithModel()
        {
            this.controller.WithCallTo(c => c.QuizResult(1))
                .ShouldRenderDefaultView()
                .WithModel<GradeViewModel>();
        }

        [TestMethod]
        public void EnrollInCourse_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.EnrollInCourse(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void AddCourseUnit_InvalidIds_BadRequest()
        {
            this.controller.WithCallTo(c => c.AddCourseUnit(-1, -1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void AddCourseUnit_Valid_RedirectToEditCourse()
        {
            this.controller.WithCallTo(c => c.AddCourseUnit(1, 1))
                .ShouldRedirectTo(c=> c.EditCourse(1));
        }

        [TestMethod]
        public void RemoveCourseUnit_InvalidIds_BadRequest()
        {
            this.controller.WithCallTo(c => c.RemoveCourseUnit(-1, -1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void RemoveCourseUnit_Valid_RedirectToEditCourse()
        {
            this.controller.WithCallTo(c => c.RemoveCourseUnit(1, 1))
                .ShouldRedirectTo(c => c.EditCourse(1));
        }

        [TestMethod]
        public void Add_Get_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.AddCourse())
                .ShouldRenderDefaultView()
                .WithModel<AddCourseViewModel>();
        }

        [TestMethod]
        public void Add_Post_RedirectToCourseList()
        {
            AddCourseBindingModel course = new AddCourseBindingModel()
            {
                Description = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                ShortDescription = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                Title = "TestCourse"
            };
            this.controller.WithCallTo(c => c.AddCourse(course))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("CourseList"));
        }

        [TestMethod]
        public void EditCourse_Get_BadRequest()
        {
            this.controller.WithCallTo(c => c.EditCourse(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void EditCourse_Get_NotFound()
        {
            this.controller.WithCallTo(c => c.EditCourse(int.MaxValue))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void EditCourse_Get_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.EditCourse(1))
                .ShouldRenderDefaultView()
                .WithModel<EditCourseViewModel>();
        }

        [TestMethod]
        public void EditCourse_Post_RedirectToDetailed()
        {
            EditCourseBindingModel model = new EditCourseBindingModel()
            {
                Id = 1,
                Description = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                ShortDescription = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                Title = "TestCourse"
            };
            this.controller.WithCallTo(c => c.EditCourse(model))
                .ShouldRedirectTo(c => c.Detailed(1));
        }
    }
}
