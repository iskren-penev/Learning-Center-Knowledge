namespace LearningCenter.Tests.Controllers
{
    using System.Net;
    using LearningCenter.App;
    using LearningCenter.App.Areas.Admin.Controllers;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Data.Mocks;
    using LearningCenter.Models.BindingModels.Units;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Implementations;
    using LearningCenter.Services.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class TestUnitsController
    {
        private UnitsController controller;
        private IUnitsService service;
        private ILearningCenterContext context;

        [TestInitialize]
        public void Initialize()
        {
            AutomapperConfig.RegisterMappings();
            this.context = new FakeLearningCenterContext();
            this.service = new UnitsService(this.context);
            this.controller = new UnitsController(this.service);

            this.context.Units.Add(new Unit()
            {
                Id=1,
                Title = "Test",
                ContentUrl = "someContent"
            });
        }

        [TestMethod]
        public void UnitPreview_BadRequest()
        {
            this.controller.WithCallTo(c => c.UnitPreview(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void UnitPreview_NotFound()
        {
            this.controller.WithCallTo(c => c.UnitPreview(10))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void UnitPreview_RendersDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.UnitPreview(1))
                .ShouldRenderDefaultView()
                .WithModel<UnitDetailsViewModel>();
        }

        [TestMethod]
        public void AddUnit_Get_RendersDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.AddUnit())
                .ShouldRenderDefaultView()
                .WithModel<AddUnitViewModel>();
        }

        [TestMethod]
        public void AddUnit_Post_RedirectToUnitsList()
        {
            this.controller.WithCallTo(c => c.AddUnit(null))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("UnitsList"));
        }

        [TestMethod]
        public void EditUnit_Get_BadRequest()
        {
            this.controller.WithCallTo(c => c.EditUnit(0))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void EditUnit_Get_NotFound()
        {
            this.controller.WithCallTo(c => c.EditUnit(int.MaxValue))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void EditUnit_Get_RendersDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.EditUnit(1))
                .ShouldRenderDefaultView()
                .WithModel<EditUnitViewModel>();
        }

        [TestMethod]
        public void EditUnit_Post_RedirectToUnitPreview()
        {
            this.controller.WithCallTo(c => c.EditUnit(new EditUnitBindingModel()
                {
                    Id = 1,
                    Title = "Title=-a",
                    ContentUrl = "some content again"
                }))
                .ShouldRedirectTo(c => c.UnitPreview(1));
        }
    }
}
