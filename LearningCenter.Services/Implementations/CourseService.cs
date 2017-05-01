namespace LearningCenter.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Courses;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Services.Interfaces;

    public class CourseService : Service, ICourseService
    {
        public IEnumerable<AllCourseViewModel> GetAllCourses()
        {
            IEnumerable<AllCourseViewModel> courses =
                Mapper.Map<IEnumerable<AllCourseViewModel>>(this.Context.Courses);
            return courses;
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
            
            this.Context.SaveChanges();
        }

        public void AddUnitToCourse(int unitId, int courseId)
        {
            Course course = this.Context.Courses.Find(courseId);
            Unit unit = this.Context.Units.Find(unitId);

            course.Units.Add(unit);
            this.Context.SaveChanges();
        }

        public void RemoveUnitFromCourse(int unitId, int courseId)
        {
            Course course = this.Context.Courses.Find(courseId);
            Unit unit = this.Context.Units.Find(unitId);

            course.Units.Remove(unit);
            this.Context.SaveChanges();
        }

        public DetailedCourseViewModel GetDetailedCourseViewModel(int id, string userId)
        {
            Course course = this.Context.Courses.Find(id);
            var units = course.Units.ToList();

            DetailedCourseViewModel viewModel = Mapper.Instance
                .Map<Course, DetailedCourseViewModel>(course);
            viewModel.Units = Mapper.Instance
                .Map<ICollection<Unit>, ICollection<UnitsInCourseListViewModel>>(units);
            if (course.Students.Any(s => s.Id == userId))
            {
                viewModel.IsCurrentUserEnrolled = true;
            }

            return viewModel;
        }

        public UnitDetailsViewModel GetUnitPreview(int unitId)
        {
            Unit unit = this.Context.Units.Find(unitId);
            UnitDetailsViewModel viewModel = Mapper.Instance
                .Map<Unit, UnitDetailsViewModel>(unit);

            return viewModel;
        }

        public void EnrollUser(string userId, int courseId)
        {
            User user = this.GetCurrentUser(userId);
            Course course = this.Context.Courses.Find(courseId);
            course.Students.Add(user);
            this.Context.SaveChanges();

        }
    }
}
