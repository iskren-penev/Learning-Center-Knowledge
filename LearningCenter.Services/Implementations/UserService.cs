namespace LearningCenter.Services.Implementations
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Models.BindingModels.User;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Models.ViewModels.User;
    using LearningCenter.Services.Interfaces;

    public class UserService : Service, IUserService
    {
        public UserService(ILearningCenterContext context) : base(context)
        {
        }

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
            if (user == null)
            {
                return null;
            }
            EditProfileViewModel viewModel = Mapper.Instance.Map<EditProfileViewModel>(user);
            return viewModel;
        }

        public ProfileViewModel GetProfileViewModel(string username)
        {
            User user = this.Context.Users.
                FirstOrDefault(u => u.UserName.StartsWith(username));
            if (user==null)
            {
                return null;
            }
            IEnumerable<Topic> topics = user.Topics;
            IEnumerable<Course> courses = user.EnrolledCourses;
            IEnumerable<Grade> grades = user.Grades;


            ProfileViewModel viewModel = Mapper.Instance
                .Map<User, ProfileViewModel>(user);

            viewModel.ForumTopics = Mapper.Instance
                .Map<IEnumerable<Topic>, IEnumerable<AllTopicsViewModel>>(topics);
            viewModel.EnrolledCourses = Mapper.Instance
                .Map<IEnumerable<Course>, IEnumerable<AllCourseViewModel>>(courses);
            viewModel.QuizResults = Mapper.Instance
                .Map<IEnumerable<Grade>, IEnumerable<GradeViewModel>>(grades);

            return viewModel;
        }
    }
}
