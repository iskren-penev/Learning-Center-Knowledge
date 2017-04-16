namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.ViewModels.Course;

    public interface ICourseService
    {
        IEnumerable<AllCourseViewModel> GetAllCourses();
    }
}