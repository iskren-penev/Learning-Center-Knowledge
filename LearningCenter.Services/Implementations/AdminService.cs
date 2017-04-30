namespace LearningCenter.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;
    using LearningCenter.Services.Interfaces;

    public class AdminService : Service, IAdminService
    {
        public IEnumerable<UserListViewModel> GetAllUsers()
        {
            IEnumerable<User> users = this.Context.Users;
            IEnumerable<UserListViewModel> viewModels = Mapper.Instance
                .Map<IEnumerable<User>, IEnumerable<UserListViewModel>>(users).ToList();

            return viewModels;
        }


        public IEnumerable<UserListViewModel> SearchUsers(string search)
        {
            var viewModels = this.GetAllUsers();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(user =>
                    user.Email.ToLower().Contains(search)
                    || (user.FirstName + " " + user.LastName).ToLower().Contains(search));
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
        
        public IEnumerable<CourseListViewModel> GetAllCourses()
        {
            IEnumerable<Course> courses = this.Context.Courses;
            IEnumerable<CourseListViewModel> viewModels = Mapper.Instance
                .Map<IEnumerable<Course>, IEnumerable<CourseListViewModel>>(courses);

            return viewModels;
        }

        public IEnumerable<CourseListViewModel> SearchCourses(string search)
        {
            IEnumerable<CourseListViewModel> viewModels = this.GetAllCourses();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model => model.Title.ToLower().Contains(search));
            }

            return viewModels;
        }

        public bool CheckUserExists(string userId)
        {
            return this.Context.Users.Find(userId) != null;
        }
    }
}

