namespace LearningCenter.Services
{
    using AutoMapper;
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.User;
    using LearningCenter.Services.Interfaces;

    public class UserService: Service, IUserService
    {
        public void EditProfile(EditProfileBindingModel model, string userId)
        {
            User currentUser = this.GetCurrentUser(userId);
            currentUser.FirstName = model.FirstName;
            currentUser.LastName = model.LastName;
            this.Context.SaveChanges();

        }

        public EditProfileViewModel GetEditProfileViewModel(string userId)
        {
            User currentUser = this.GetCurrentUser(userId);
            EditProfileViewModel viewModel = Mapper.Map<EditProfileViewModel>(currentUser);
            return viewModel;
        }

        public ProfileViewModel GetProfileViewModel(string userId)
        {
            User currentUser = this.GetCurrentUser(userId);
            ProfileViewModel viewModel = Mapper.Map<ProfileViewModel>(currentUser);
            return viewModel;
        }
    }
}
