namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.BindingModels.Admin;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;

    public interface IAdminService
    {
        IEnumerable<UserListViewModel> GetAllUsers();

        IEnumerable<UserListViewModel> SearchUsers(string search);

        void SetRoleNameForModel(UserListViewModel model, List<string> roleNames);

        User GetCurrentUserByEmail(string email);
        
        void AddCourse(AddCourseBindingModel model);

        void AddRoleToUser(string userId);
        
        void AddQuiz();

        IEnumerable<CourseListViewModel> GetAllCourses();

        IEnumerable<CourseListViewModel> SearchCourses(string search);

        EditCourseViewModel GetEditCourseViewModel(int id);

        AddCourseViewModel GetAddCourseViewModel(AddCourseBindingModel model);

        void EditCourse(EditCourseBindingModel model);

        IEnumerable<UnitListViewModel> GetAllUnits();

        IEnumerable<UnitListViewModel> SearchUnits(string search);

        UnitDetailsViewModel GetUnitDetailsViewModel(int id);

        void AddUnit(AddUnitBindingModel model);

        AddUnitViewModel GetAddUnitViewModel(AddUnitBindingModel model);

        EditUnitViewModel GetEditUnitViewModel(int id);
        void EditUnit(EditUnitBindingModel model);
    }
}
