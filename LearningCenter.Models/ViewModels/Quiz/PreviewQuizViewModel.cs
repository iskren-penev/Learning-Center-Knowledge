namespace LearningCenter.Models.ViewModels.Quiz
{
    using System.Collections.Generic;

    public class PreviewQuizViewModel
    {
        public PreviewQuizViewModel()
        {
            this.Questions = new List<PreviewQuestionViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<PreviewQuestionViewModel> Questions { get; set; }
    }
}
