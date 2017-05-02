namespace LearningCenter.Models.BindingModels.Quiz
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditQuestionBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
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
        [Range(1,3)]
        public int CorrectAnswer { get; set; }
    }
}
