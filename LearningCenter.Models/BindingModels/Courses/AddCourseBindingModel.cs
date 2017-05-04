namespace LearningCenter.Models.BindingModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    public class AddCourseBindingModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and 50 characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 50, ErrorMessage = "The {0} must be between {2} and 300 characters long.")]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 250, ErrorMessage = "The {0} must be between {2} and 10000 characters long.")]
        public string Description { get; set; }
    }
}
