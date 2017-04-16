namespace LearningCenter.Services.Interfaces
{
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.ViewModels.User;

    public interface IUserService
    {
        void EditProfile(EditProfileBindingModel model, string userId);

        EditProfileViewModel GetEditProfileViewModel(string userId);

        ProfileViewModel GetProfileViewModel(string userId);
    }
}
