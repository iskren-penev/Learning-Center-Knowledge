namespace LearningCenter.Models.BindingModels.Quiz
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditQuestionBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Description { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int AnswerOneId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int AnswerTwoId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int AnswerThreeId { get; set; }

        [Required]
        public string OptionOne { get; set; }

        [Required]
        public string OptionTwo { get; set; }

        [Required]
        public string OptionThree { get; set; }

        [Required]
        [Range(1,3, ErrorMessage = "The correct answer must be between 1 and 3.")]
        public int CorrectAnswer { get; set; }
    }
}
