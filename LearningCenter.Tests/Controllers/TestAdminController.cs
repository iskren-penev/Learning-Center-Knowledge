namespace LearningCenter.Tests.Controllers
{
    using System.Collections.Generic;
    using LearningCenter.App;
    using LearningCenter.App.Areas.Admin.Controllers;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Data.Mocks;
    using LearningCenter.Models.ViewModels.Admin;
    using LearningCenter.Services.Implementations;
    using LearningCenter.Services.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class TestAdminController
    {
        private AdminController controller;
        private IAdminService service;
        private ILearningCenterContext context;

        [TestInitialize]
        public void Initialize()
        {
            AutomapperConfig.RegisterMappings();
            this.context = new FakeLearningCenterContext();
            this.service = new AdminService(this.context);
            this.controller = new AdminController(this.service);

        }

        [TestMethod]
        public void UsersList_RendersDefaultView()
        {
            this.controller.WithCallTo(c => c.UsersList())
                .ShouldRenderDefaultView()
                .WithModel<List<UserListViewModel>>();
        }

        [TestMethod]
        public void SearchUsers_RendersPartialView()
        {
            this.controller.WithCallTo(c => c.SearchUsers(null))
                .ShouldRenderPartialView("_SearchUsers")
                .WithModel<List<UserListViewModel>>();
        }

        [TestMethod]
        public void CourseList_RendersDefaultView()
        {
            this.controller.WithCallTo(c => c.CourseList())
                .ShouldRenderDefaultView()
                .WithModel<List<CourseListViewModel>>();
        }

        [TestMethod]
        public void SearchCourses_RendersPartialView()
        {
            this.controller.WithCallTo(c => c.SearchCourses(null))
                .ShouldRenderPartialView("_SearchCourses")
                .WithModel<List<CourseListViewModel>>();
        }

        [TestMethod]
        public void UnitList_RendersDefaultView()
        {
            this.controller.WithCallTo(c => c.UnitsList())
                .ShouldRenderDefaultView()
                .WithModel<List<UnitListViewModel>>();
        }

        [TestMethod]
        public void SearchUnits_RendersPartialView()
        {
            this.controller.WithCallTo(c => c.SearchUnits(null))
                .ShouldRenderPartialView("_SearchUnits")
                .WithModel<List<UnitListViewModel>>();
        }

        [TestMethod]
        public void QuizList_RendersDefaultView()
        {
            this.controller.WithCallTo(c => c.QuizList())
                .ShouldRenderDefaultView()
                .WithModel<List<QuizListViewModel>>();
        }

        [TestMethod]
        public void SearchQuizzes_RendersPartialView()
        {
            this.controller.WithCallTo(c => c.SearchQuizzes(null))
                .ShouldRenderPartialView("_SearchQuizzes")
                .WithModel<List<QuizListViewModel>>();
        }

        [TestMethod]
        public void QuestionList_RendersDefaultView()
        {
            this.controller.WithCallTo(c => c.QuestionList())
                .ShouldRenderDefaultView()
                .WithModel<List<QuestionListViewModel>>();
        }

        [TestMethod]
        public void SearchQuestions_RendersPartialView()
        {
            this.controller.WithCallTo(c => c.SearchQuestions(null))
                .ShouldRenderPartialView("_SearchQuestions")
                .WithModel<List<QuestionListViewModel>>();
        }
    }
}
