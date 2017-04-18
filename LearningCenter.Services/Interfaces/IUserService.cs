namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.User;

    public interface IUserService
    {
        void EditProfile(EditProfileBindingModel model, string userId);

        EditProfileViewModel GetEditProfileViewModel(string username);

        ProfileViewModel GetProfileViewModel(string username);

        IEnumerable<AllUserViewModel> GetAllUsers(string search);

        User GetCurrentUserByEmail(string email);

        void SetRoleNameForModel(AllUserViewModel model, List<string> roleNames);
    }
}
