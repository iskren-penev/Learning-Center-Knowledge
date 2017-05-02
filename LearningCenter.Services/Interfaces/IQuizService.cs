namespace LearningCenter.Services.Interfaces
{
    using LearningCenter.Models.BindingModels.Quiz;
    using LearningCenter.Models.ViewModels.Quiz;

    public interface IQuizService
    {
        void AddNewQuiz(AddQuizBindingModel model);

        AddQuizViewModel GetAddViewModel(AddQuizBindingModel model);

        EditQuizViewModel GetEditViewModel(int id);

        void AddQuestion(AddQuestionBindingModel model);

        EditQuestionViewModel GetEditQuestionViewModel(int id);

        EditQuestionViewModel GetEditQuestionViewModel(EditQuestionBindingModel model);

        void EditQuestion(EditQuestionBindingModel model);

        PreviewQuestionViewModel GetPreviewQuestionViewModel(int id);

        void EditQuiz(EditQuizBindingModel model);

        PreviewQuizViewModel GetPreviewQuizViewModel(int id);

        void RemoveQuestionFromQuiz(int questionId, int quizId);

        void AddQuestionToQuiz(int questionId, int quizId);
    }
}
