namespace LearningCenter.Services.Implementations
{
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Units;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Interfaces;

    public class UnitsService : Service, IUnitsService
    {
        public UnitDetailsViewModel GetUnitDetailsViewModel(int id)
        {
            Unit unit = this.Context.Units.Find(id);
            if (unit == null)
            {
                return null;
            }
            UnitDetailsViewModel viewModel = Mapper.Instance.Map<Unit, UnitDetailsViewModel>(unit);
            if (unit.CourseId != null)
            {
                viewModel.CourseName = unit.Course.Title;
            }
            else
            {
                viewModel.CourseName = "Unassigned";
            }

            return viewModel;
        }

        public void AddUnit(AddUnitBindingModel model)
        {
            Unit unit = Mapper.Instance.Map<AddUnitBindingModel, Unit>(model);

            this.Context.Units.Add(unit);
            this.Context.SaveChanges();
        }

        public AddUnitViewModel GetAddUnitViewModel(AddUnitBindingModel model)
        {
            AddUnitViewModel viewModel = Mapper.Instance
                .Map<AddUnitBindingModel, AddUnitViewModel>(model);

            return viewModel;
        }

        public EditUnitViewModel GetEditUnitViewModel(int id)
        {
            Unit unit = this.Context.Units.Find(id);
            if (unit == null)
            {
                return null;
            }
            EditUnitViewModel viewModel = Mapper.Instance
                .Map<Unit, EditUnitViewModel>(unit);

            return viewModel;
        }

        public void EditUnit(EditUnitBindingModel model)
        {
            Unit unit = this.Context.Units.Find(model.Id);
            if (unit == null)
            {
                return;
            }
            unit.Title = model.Title;
            unit.ContentUrl = model.ContentUrl;

            this.Context.SaveChanges();
        }
    }
}
