namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;

    public interface IAdminService
    {
        IEnumerable<AllUserViewModel> GetAllUsers();

        IEnumerable<AllUserViewModel> SearchUsers(string search);

        void SetRoleNameForModel(AllUserViewModel model, List<string> roleNames);

        User GetCurrentUserByEmail(string email);


        void AddCourse();
        void AddRoleToUser(string userId);
        void AddNewUser();
        void AddQuiz();
    }
}
