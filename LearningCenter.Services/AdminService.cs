namespace LearningCenter.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;
    using LearningCenter.Services.Interfaces;

    public class AdminService : Service, IAdminService
    {
        public IEnumerable<AllUserViewModel> GetAllUsers()
        {
            IEnumerable<User> users = this.Context.Users;
            IEnumerable<AllUserViewModel> viewModels = Mapper.Instance
                .Map<IEnumerable<User>, IEnumerable<AllUserViewModel>>(users).ToList();

            return viewModels;
        }


        public IEnumerable<AllUserViewModel> SearchUsers(string search)
        {
            var viewModels = this.GetAllUsers();
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
        public void AddCourse()
        {
            throw new System.NotImplementedException();
        }

        public void AddRoleToUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void AddNewUser()
        {
            throw new System.NotImplementedException();
        }

        public void AddQuiz()
        {
            throw new System.NotImplementedException();
        }
    }
}
