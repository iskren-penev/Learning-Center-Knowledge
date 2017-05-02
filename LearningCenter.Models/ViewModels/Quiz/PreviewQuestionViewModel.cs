namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.Collections.Generic;

    public class PreviewQuestionViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}
