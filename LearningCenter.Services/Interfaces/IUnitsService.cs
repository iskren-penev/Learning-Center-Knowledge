namespace LearningCenter.Services.Interfaces
{
    using LearningCenter.Models.BindingModels.Units;
    using LearningCenter.Models.ViewModels.Units;

    public interface IUnitsService
    {
        UnitDetailsViewModel GetUnitDetailsViewModel(int id);

        void AddUnit(AddUnitBindingModel model);

        AddUnitViewModel GetAddUnitViewModel(AddUnitBindingModel model);

        EditUnitViewModel GetEditUnitViewModel(int id);

        void EditUnit(EditUnitBindingModel model);

    }
}
