namespace LearningCenter.Services
{
    using System.Collections.Generic;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Services.Interfaces;

    public class CourseService : Service, ICourseService
    {
        public IEnumerable<AllCourseViewModel> GetAllCourses()
        {
            IEnumerable<AllCourseViewModel> courses =
                AutoMapper.Mapper.Map<IEnumerable<AllCourseViewModel>>(this.Context.Courses);
            return courses;
        }
    }
}
