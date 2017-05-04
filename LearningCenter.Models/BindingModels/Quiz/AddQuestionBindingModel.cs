namespace LearningCenter.Models.BindingModels.Quiz
{
    using System.ComponentModel.DataAnnotations;

    public class AddQuestionBindingModel
    {
        [Required]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Descrioption { get; set; }

        [Required]
        public string OptionOne { get; set; }

        [Required]
        public string OptionTwo { get; set; }

        [Required]
        public string OptionThree { get; set; }

        [Required]
        [Range(1,3,ErrorMessage = "Set the number of the correct answer")]
        public int CorrectAnswer { get; set; }
    }
}
