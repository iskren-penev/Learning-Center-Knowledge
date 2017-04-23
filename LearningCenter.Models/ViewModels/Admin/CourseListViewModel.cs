namespace LearningCenter.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class CourseListViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        [Display(Name = "Students in course")]
        public int Students { get; set; }

    }
}
