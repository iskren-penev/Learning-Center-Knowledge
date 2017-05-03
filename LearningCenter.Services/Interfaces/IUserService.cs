namespace LearningCenter.Services.Interfaces
{
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.ViewModels.User;

    public interface IUserService : IService
    {
        void EditProfile(EditProfileBindingModel model, string userId);

        EditProfileViewModel GetEditProfileViewModel(string username);

        ProfileViewModel GetProfileViewModel(string username);
    }
}
