namespace LearningCenter.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;
    using LearningCenter.Services.Interfaces;

    public class AdminService : Service, IAdminService
    {
        public AdminService(ILearningCenterContext context) : base(context)
        {
        }

        public List<UserListViewModel> GetAllUsers()
        {
            List<User> users = this.Context.Users.ToList();
            List<UserListViewModel> viewModels = Mapper.Instance
                .Map<List<User>, List<UserListViewModel>>(users);

            return viewModels;
        }


        public List<UserListViewModel> SearchUsers(string search)
        {
            var viewModels = this.GetAllUsers();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(user =>
                    user.Email.ToLower().Contains(search)
                    || (user.FirstName + " " + user.LastName).ToLower().Contains(search)).ToList();
            }

            return viewModels;
        }

        public void SetRoleNameForModel(UserListViewModel model, List<string> roleNames)
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
        
        public List<CourseListViewModel> GetAllCourses()
        {
            List<Course> courses = this.Context.Courses.ToList();
            List<CourseListViewModel> viewModels = Mapper.Instance
                .Map<List<Course>, List<CourseListViewModel>>(courses);

            return viewModels;
        }

        public List<CourseListViewModel> SearchCourses(string search)
        {
            List<CourseListViewModel> viewModels = this.GetAllCourses();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model => model.Title.ToLower().Contains(search)).ToList();
            }

            return viewModels;
        }

        public bool CheckUserExists(string userId)
        {
            return this.Context.Users.Find(userId) != null;
        }


       
    }
}

