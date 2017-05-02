namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;

    public class AddQuestionViewModel
    {
        public string Descrioption { get; set; }

        [Display(Name = "Answer 1:")]
        public string OptionOne { get; set; }

        [Display(Name = "Answer 2:")]
        public string OptionTwo { get; set; }

        [Display(Name = "Answer 3:")]
        public string OptionThree { get; set; }
        
        public int CorrectAnswer { get; set; }
    }
}
