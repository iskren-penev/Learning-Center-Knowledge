namespace LearningCenter.Models.ViewModels.Course
{
    using System.ComponentModel.DataAnnotations;

    public class AllCourseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        

        public string ShortDescription { get; set; }

        [Display(Name = "Students in course")]
        public int StudentsInCourse { get; set; }
    }
}
