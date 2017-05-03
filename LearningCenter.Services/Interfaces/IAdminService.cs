namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;

    public interface IAdminService : IService
    {
        List<UserListViewModel> GetAllUsers();

        List<UserListViewModel> SearchUsers(string search);

        void SetRoleNameForModel(UserListViewModel model, List<string> roleNames);

        User GetCurrentUserByEmail(string email);

        List<CourseListViewModel> GetAllCourses();

        List<CourseListViewModel> SearchCourses(string search);

        List<UnitListViewModel> GetAllUnits();

        List<UnitListViewModel> SearchUnits(string search);

        bool CheckUserExists(string userId);

        List<QuizListViewModel> GetAllQuizzes();

        List<QuizListViewModel> SearchQuizzes(string search);

        List<QuestionListViewModel> GetAllQuestions();

        List<QuestionListViewModel> SearchQuestions(string search);
    }
}
