using System;
namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;

    public class EditQuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "The {0} must be at least {2} characters long.")]
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
        [Display(Name = "Answer 1:")]
        public string OptionOne { get; set; }

        [Required]
        [Display(Name = "Answer 2:")]
        public string OptionTwo { get; set; }

        [Required]
        [Display(Name = "Answer 3:")]
        public string OptionThree { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "The correct answer must be between 1 and 3.")]
        public int CorrectAnswer { get; set; }
    }
}
