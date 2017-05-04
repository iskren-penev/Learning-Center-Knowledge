namespace LearningCenter.Tests.Controllers
{
    using System;
    using System.Net;
    using LearningCenter.App;
    using LearningCenter.App.Areas.Forum.Controllers;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Data.Mocks;
    using LearningCenter.Models.BindingModels.Forum;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Services.Implementations;
    using LearningCenter.Services.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PagedList;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class TestForumController
    {
        private ForumController controller;
        private IForumService service;
        private ILearningCenterContext context;

        [TestInitialize]
        public void Initialize()
        {
            AutomapperConfig.RegisterMappings();
            this.context = new FakeLearningCenterContext();
            this.service = new ForumService(this.context);
            this.controller = new ForumController(this.service);

            Tag tag = new Tag()
            {
                Id = 1, Name = "Tagtest"
            };
            Topic topic = new Topic()
            {
                Id = 1,
                Title = "Test",
                Content =
                    "Lorem ipsum dolor sit amet,consectetur adipiscing elit,sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                PublishDate = DateTime.Today
            };
            tag.Topics.Add(topic);
            topic.Tags.Add(tag);
            this.context.Topics.Add(topic);
            this.context.Tags.Add(tag);
        }

        [TestMethod]
        public void All_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.All())
                .ShouldRenderDefaultView()
                .WithModel<ForumListViewModel>();
        }

        [TestMethod]
        public void Display_RenderPartialViewWithModel()
        {
            this.controller.WithCallTo(c => c.Display("", 1))
                .ShouldRenderPartialView("_DisplayTopics")
                .WithModel<IPagedList<AllTopicsViewModel>>();
        }

        [TestMethod]
        public void Detailed_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.Detailed(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
        
        [TestMethod]
        public void Detailed_Valid_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.Detailed(1))
                .ShouldRenderDefaultView()
                .WithModel<DetailedTopicViewModel>();
        }

        [TestMethod]
        public void Add_Get_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.Add())
                .ShouldRenderDefaultView()
                .WithModel<AddTopicViewModel>();
        }

        [TestMethod]
        public void Add_Post_RedirectToAllTopics()
        {
            // the user is always null because we get his Id from the IPrincipal
            AddTopicBindingModel model = new AddTopicBindingModel()
            {
                Content = "Lorem ipsum dolor sit amet,consectetur adipiscing elit,sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Title = "SomeTak",
                Tags = "add,tags,here"
            };
            this.controller.WithCallTo(c => c.Add(model))
                .ShouldRedirectTo(c => c.All);
        }

        [TestMethod]
        public void EditTopic_Get_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.EditTopic(0))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

       
        [TestMethod]
        public void EditTopic_Get_Valid_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.EditTopic(1))
                .ShouldRenderDefaultView()
                .WithModel<EditTopicViewModel>();
        }

        [TestMethod]
        public void EditTopic_Post_RedirectToDetailed()
        {
            EditTopicBindingModel model = new EditTopicBindingModel()
            {
                Content = "Lorem ipsum dolor sit amet,consectetur adipiscing elit,sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Id = 1,
                Tags = "add,tags,here"
            };
            this.controller.WithCallTo(c => c.EditTopic(model))
                .ShouldRedirectTo(c => c.Detailed(1));
        }

        [TestMethod]
        public void AddReply_Get_RenderPartialView()
        {
            this.controller.WithCallTo(c => c.AddReply())
                .ShouldRenderPartialView("_AddReply");
        }
    }
}
