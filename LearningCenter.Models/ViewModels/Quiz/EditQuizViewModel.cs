namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.Collections.Generic;
    using LearningCenter.Models.ViewModels.Admin;

    public class EditQuizViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int NumberOfQuestionsInQuiz { get; set; }

        public ICollection<QuestionListViewModel> QuestionsInQuiz { get; set; }

        public ICollection<QuestionListViewModel> UnassignedQuestions { get; set; }

    }
}
