namespace LearningCenter.Models.ViewModels.Course
{
    using System.ComponentModel.DataAnnotations;

    public class AddCourseViewModel
    {
        public string Title { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        
        public string Description { get; set; }
    }
}
