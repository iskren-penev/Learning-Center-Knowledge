namespace LearningCenter.Tests.Controllers
{
    using LearningCenter.App;
    using LearningCenter.App.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class TestHomeController
    {
        private HomeController controller;

        [TestInitialize]
        public void Initialize()
        {
            AutomapperConfig.RegisterMappings();
            this.controller = new HomeController();
        }

        [TestMethod]
        public void Index_ReturnsDefaultView()
        {
            this.controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}
