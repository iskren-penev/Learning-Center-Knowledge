namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LearningCenter.Models.ViewModels.Admin;

    public class EditQuizViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Title { get; set; }

        public int NumberOfQuestionsInQuiz { get; set; }

        public ICollection<QuestionListViewModel> QuestionsInQuiz { get; set; }

        public ICollection<QuestionListViewModel> UnassignedQuestions { get; set; }

    }
}
