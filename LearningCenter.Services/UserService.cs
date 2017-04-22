namespace LearningCenter.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Services.Interfaces;
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Models.ViewModels.User;

    public class UserService : Service, IUserService
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
            User user = this.Context.Users.
                FirstOrDefault(u => u.UserName.StartsWith(username));
            IEnumerable<Topic> topics = this.Context.Topics
                .Where(topic => topic.Author.UserName == user.UserName);
            IEnumerable<Course> courses = this.Context.Courses
                .Where(course => course.Students.Any(u => u.UserName == user.UserName));

            ProfileViewModel viewModel = Mapper.Instance
                .Map<User, ProfileViewModel>(user);
            viewModel.ForumTopics = Mapper.Instance
                .Map<IEnumerable<Topic>, IEnumerable<AllTopicsViewModel>>(topics);
            viewModel.EnrolledCourses = Mapper.Instance
                .Map<IEnumerable<Course>, IEnumerable<AllCourseViewModel>>(courses);

            return viewModel;
        }

        
    }
}
