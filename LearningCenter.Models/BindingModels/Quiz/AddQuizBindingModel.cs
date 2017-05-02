namespace LearningCenter.Models.BindingModels.Quiz
{
    using System.ComponentModel.DataAnnotations;

    public class AddQuizBindingModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }
    }
}
