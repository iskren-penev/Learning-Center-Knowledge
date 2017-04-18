namespace LearningCenter.Services
{
    using System.Collections;
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
                .Where(topic => topic.Author.UserName == username);
            IEnumerable<Course> courses = this.Context.Courses
                .Where(course => course.Students.Any(u => u.UserName == username));

            ProfileViewModel viewModel = Mapper.Instance
                .Map<User, ProfileViewModel>(user);
            viewModel.ForumTopics = Mapper.Instance
                .Map<IEnumerable<Topic>, IEnumerable<AllTopicsViewModel>>(topics);
            viewModel.EnrolledCourses = Mapper.Instance
                .Map<IEnumerable<Course>, IEnumerable<AllCourseViewModel>>(courses);

            return viewModel;
        }

        public IEnumerable<AllUserViewModel> GetAllUsers(string search)
        {
            IEnumerable<User> users = this.Context.Users;
            IEnumerable<AllUserViewModel> viewModels = Mapper.Instance
                .Map<IEnumerable<User>, IEnumerable<AllUserViewModel>>(users).ToList();
            
            if (search != null)
            {
                search = search.ToLower();
                viewModels = viewModels.Where(user =>
                    user.Email.ToLower().Contains(search)
                    || (user.FirstName + " " + user.LastName).ToLower().Contains(search));
            }
            return viewModels;
        }

        public void SetRoleNameForModel(AllUserViewModel model, List<string> roleNames)
        {
            foreach (string role in roleNames)
            {
                model.Roles.Add(role);
            }
        }
        public User GetCurrentUserByEmail(string email)
        {
            return this.Context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
