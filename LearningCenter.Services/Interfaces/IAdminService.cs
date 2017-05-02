namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;

    public interface IAdminService
    {
        IEnumerable<UserListViewModel> GetAllUsers();

        IEnumerable<UserListViewModel> SearchUsers(string search);

        void SetRoleNameForModel(UserListViewModel model, List<string> roleNames);

        User GetCurrentUserByEmail(string email);
        
        IEnumerable<CourseListViewModel> GetAllCourses();

        IEnumerable<CourseListViewModel> SearchCourses(string search);

        IEnumerable<UnitListViewModel> GetAllUnits();

        IEnumerable<UnitListViewModel> SearchUnits(string search);

        bool CheckUserExists(string userId);

        IEnumerable<QuizListViewModel> GetAllQuizzes();

        IEnumerable<QuizListViewModel> SearchQuizzes(string search);

        IEnumerable<QuestionListViewModel> GetAllQuestions();
        
        IEnumerable<QuestionListViewModel> SearchQuestions(string search);
    }
}
