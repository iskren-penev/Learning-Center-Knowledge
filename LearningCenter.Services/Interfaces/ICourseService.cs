namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.BindingModels.Courses;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Units;

    public interface ICourseService
    {
        IEnumerable<AllCourseViewModel> GetAllCourses();

        EditCourseViewModel GetEditCourseViewModel(int id);

        AddCourseViewModel GetAddCourseViewModel(AddCourseBindingModel model);

        void AddCourse(AddCourseBindingModel model);

        void EditCourse(EditCourseBindingModel model);

        void AddUnitToCourse(int unitId, int courseId);

        void RemoveUnitFromCourse(int unitId, int courseId);

        DetailedCourseViewModel GetDetailedCourseViewModel(int id, string userId);

        UnitDetailsViewModel GetUnitPreview(int unitId);

        void EnrollUser(string userId, int courseId);
    }
}