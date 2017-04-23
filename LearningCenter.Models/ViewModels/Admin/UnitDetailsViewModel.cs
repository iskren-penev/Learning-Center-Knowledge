namespace LearningCenter.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class UnitDetailsViewModel
    {
        public string Title { get; set; }
        
        public string ContentUrl { get; set; }

        [Display(Name = "Course")]
        public string CourseName { get; set; }

    }
}
