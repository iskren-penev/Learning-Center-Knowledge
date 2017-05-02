using System;
namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;

    public class EditQuestionViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int AnswerOneId { get; set; }

        public int AnswerTwoId { get; set; }

        public int AnswerThreeId { get; set; }

        [Display(Name = "Answer 1:")]
        public string OptionOne { get; set; }

        [Display(Name = "Answer 2:")]
        public string OptionTwo { get; set; }

        [Display(Name = "Answer 3:")]
        public string OptionThree { get; set; }

        public int CorrectAnswer { get; set; }
    }
}
