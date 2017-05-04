namespace LearningCenter.Services.Implementations
{
    using System;
    using AutoMapper;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Models.BindingModels.Units;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Interfaces;

    public class UnitsService : Service, IUnitsService
    {
        public UnitsService(ILearningCenterContext context) : base(context)
        {
        }

        public UnitDetailsViewModel GetUnitDetailsViewModel(int id)
        {
            Unit unit = this.Context.Units.Find(id);
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(id), "There is no Unit with such Id.");
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
                throw new ArgumentNullException(nameof(id), "There is no Unit with such Id.");
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
                throw new ArgumentNullException(nameof(model.Id), "There is no Unit with such Id.");
            }
            unit.Title = model.Title;
            unit.ContentUrl = model.ContentUrl;

            this.Context.SaveChanges();
        }
    }
}
