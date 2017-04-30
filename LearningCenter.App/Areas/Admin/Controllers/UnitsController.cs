namespace LearningCenter.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.Units;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Interfaces;

    [Authorize(Roles = "Admin,Instructor")]
    [RouteArea("admin")]
    public class UnitsController : Controller
    {
        private IUnitsService service;

        public UnitsController(IUnitsService service)
        {
            this.service = service;
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
            return this.View(new AddUnitViewModel());
        }

        [HttpPost]
        [Route("units/add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddUnit([Bind(Include = "Title,ContentUrl")]AddUnitBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddUnit(model);
                return this.RedirectToAction("UnitsList", "Admin");
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
        public ActionResult EditUnit([Bind(Include = "Id,Title,ContentUrl")] EditUnitBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditUnit(model);
                return this.RedirectToAction("UnitPreview", new { id = model.Id });
            }

            EditUnitViewModel viewModel = this.service.GetEditUnitViewModel(model.Id);
            return this.View(viewModel);
        }
    }
}
