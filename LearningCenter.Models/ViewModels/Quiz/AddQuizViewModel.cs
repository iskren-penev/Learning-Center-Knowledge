namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;

    public class AddQuizViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Title { get; set; }
    }
}
