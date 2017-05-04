namespace LearningCenter.Models.ViewModels.Course
{
    using System.ComponentModel.DataAnnotations;

    public class AddCourseViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 50, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 250, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Description { get; set; }
    }
}
