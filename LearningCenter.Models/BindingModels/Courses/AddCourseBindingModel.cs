namespace LearningCenter.Models.BindingModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    public class AddCourseBindingModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string ShortDescription { get; set; }

        [Required]
        [MinLength(250)]
        public string Description { get; set; }
    }
}
