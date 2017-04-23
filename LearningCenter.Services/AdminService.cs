namespace LearningCenter.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Admin;
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
        
        public void AddRoleToUser(string userId)
        {
            throw new System.NotImplementedException();
        }
        

        public void AddQuiz()
        {
            throw new System.NotImplementedException();
        }

        public void AddCourse(AddCourseBindingModel model)
        {
            Course course = Mapper.Instance.Map<AddCourseBindingModel, Course>(model);

            this.Context.Courses.Add(course);
            this.Context.SaveChanges();
        }

        public AddCourseViewModel GetAddCourseViewModel(AddCourseBindingModel model)
        {
            AddCourseViewModel viewModel = Mapper.Instance
                .Map<AddCourseBindingModel, AddCourseViewModel>(model);

            return viewModel;
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

        public EditCourseViewModel GetEditCourseViewModel(int id)
        {
            Course course = this.Context.Courses.Find(id);
            EditCourseViewModel viewModel = Mapper.Instance.Map<Course, EditCourseViewModel>(course);

            viewModel.UnitsInCourse = this.SearchUnits(course.Title).ToList();
            viewModel.UnassignedUnits = this.SearchUnits("Unassigned").ToList();

            return viewModel;
        }

        public void EditCourse(EditCourseBindingModel model)
        {
            Course course = this.Context.Courses.Find(model.Id);

            course.Title = model.Title;
            course.ShortDescription = model.ShortDescription;
            course.Description = model.Description;

            var unitIds = model.UnitIds.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            foreach (int id in unitIds)
            {
                Unit currentUnit = this.Context.Units.Find(id);
                course.Units.Add(currentUnit);
            }

            this.Context.SaveChanges();
        }

        public IEnumerable<UnitListViewModel> GetAllUnits()
        {
            IEnumerable<Unit> units = this.Context.Units;
            var viewModels = Mapper.Instance
                .Map<IEnumerable<Unit>, IEnumerable<UnitListViewModel>>(units).ToArray();

            for (int i = 0; i < viewModels.Length; i++)
            {
                var unit = units.FirstOrDefault(u => u.Id == viewModels[i].Id);
                if (unit.CourseId != null)
                {
                    viewModels[i].CourseName = unit.Course.Title;
                }
                else
                {
                    viewModels[i].CourseName = "Unassigned";
                }
            }

            return viewModels;
        }

        public IEnumerable<UnitListViewModel> SearchUnits(string search)
        {
            IEnumerable<UnitListViewModel> viewModels = this.GetAllUnits();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model =>
                    model.Title.ToLower().Contains(search)
                    || model.CourseName.ToLower().Contains(search));
            }

            return viewModels;
        }

        public UnitDetailsViewModel GetUnitDetailsViewModel(int id)
        {
            Unit unit = this.Context.Units.Find(id);
            UnitDetailsViewModel viewModel = Mapper.Instance.Map<Unit, UnitDetailsViewModel>(unit);
            if (unit.CourseId !=null)
            {
                viewModel.CourseName = unit.Course.Title;
            }
            else
            {
                viewModel.CourseName = "Unassigned";
            }

            return viewModel;
        }

        public void AddUnit(AddUnitBindingModel model)
        {
            Unit unit = Mapper.Instance.Map<AddUnitBindingModel, Unit>(model);

            this.Context.Units.Add(unit);
            this.Context.SaveChanges();
        }

        public AddUnitViewModel GetAddUnitViewModel(AddUnitBindingModel model)
        {
            AddUnitViewModel viewModel = Mapper.Instance
                .Map<AddUnitBindingModel, AddUnitViewModel>(model);

            return viewModel;
        }

        public EditUnitViewModel GetEditUnitViewModel(int id)
        {
            Unit unit = this.Context.Units.Find(id);
            EditUnitViewModel viewModel = Mapper.Instance
                .Map<Unit, EditUnitViewModel>(unit);

            return viewModel;
        }

        public void EditUnit(EditUnitBindingModel model)
        {
            Unit unit = this.Context.Units.Find(model.Id);
            unit.Title = model.Title;
            unit.ContentUrl = model.ContentUrl;

            this.Context.SaveChanges();
        }
    }
}

