namespace LearningCenter.Services
{
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Services.Interfaces;
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.User;

    public class UserService: Service, IUserService
    {
        public void EditProfile(EditProfileBindingModel model, string userId)
        {
            User currentUser = this.GetCurrentUser(userId);
            currentUser.FirstName = model.FirstName;
            currentUser.LastName = model.LastName;
            this.Context.SaveChanges();

        }

        public EditProfileViewModel GetEditProfileViewModel(string username)
        {
            User user = this.Context.Users.FirstOrDefault(u => u.UserName.StartsWith(username));

            EditProfileViewModel viewModel = Mapper.Instance.Map<EditProfileViewModel>(user);
            return viewModel;
        }

        public ProfileViewModel GetProfileViewModel(string username)
        {
            User user = this.Context.Users.FirstOrDefault(u => u.UserName.StartsWith(username));

            ProfileViewModel viewModel = Mapper.Instance.Map<ProfileViewModel>(user);
            return viewModel;
        }
    }
}
