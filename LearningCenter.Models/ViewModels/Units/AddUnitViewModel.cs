namespace LearningCenter.Models.ViewModels.Units
{
    using System.ComponentModel.DataAnnotations;

    public class AddUnitViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content URL")]
        public string ContentUrl { get; set; }
    }
}
