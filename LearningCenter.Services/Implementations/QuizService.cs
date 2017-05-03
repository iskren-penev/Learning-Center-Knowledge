namespace LearningCenter.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Quiz;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Quiz;
    using LearningCenter.Services.Interfaces;

    public class QuizService : Service, IQuizService
    {
        public void AddNewQuiz(AddQuizBindingModel model)
        {
            Quiz quiz = Mapper.Instance
                .Map<AddQuizBindingModel, Quiz>(model);
        }

        public AddQuizViewModel GetAddViewModel(AddQuizBindingModel model)
        {
            AddQuizViewModel viewModel = Mapper.Instance
                .Map<AddQuizBindingModel, AddQuizViewModel>(model);

            return viewModel;
        }

        public EditQuizViewModel GetEditViewModel(int id)
        {
            Quiz quiz = this.GetQuiz(id);
            if (quiz==null)
            {
                return null;
            }
            EditQuizViewModel viewModel = Mapper.Instance
                .Map<Quiz, EditQuizViewModel>(quiz);

            viewModel.QuestionsInQuiz = this.SearchQuestions(quiz.Title).ToList();
            viewModel.UnassignedQuestions = this.SearchQuestions("Unassigned").ToList();

            return viewModel;
        }

        public void AddQuestion(AddQuestionBindingModel model)
        {
            Question question = Mapper.Instance
                .Map<AddQuestionBindingModel, Question>(model);

            bool firstCorrect = model.CorrectAnswer == 1;
            bool secondCorrect = model.CorrectAnswer == 2;
            bool thirdCorrect = model.CorrectAnswer == 3;
            Answer firstAnswer = this.AddAnswer(model.OptionOne, firstCorrect);
            Answer secondAnswer = this.AddAnswer(model.OptionTwo, secondCorrect);
            Answer thirdAnswer = this.AddAnswer(model.OptionThree, thirdCorrect);

            question.Answers.Add(firstAnswer);
            question.Answers.Add(secondAnswer);
            question.Answers.Add(thirdAnswer);

            this.Context.Questions.Add(question);
            this.Context.SaveChanges();
        }

        public EditQuestionViewModel GetEditQuestionViewModel(int id)
        {
            Question question = this.GetQuestion(id);
            if (question == null)
            {
                return null;
            }
            EditQuestionViewModel viewModel = new EditQuestionViewModel()
            {
                Id = question.Id,
                Description = question.Description
            };

            Answer[] answers = question.Answers.ToArray();
            for (int i = 0; i < 3; i++)
            {
                if (answers[i].IsCorrect)
                {
                    viewModel.CorrectAnswer = i + 1;
                }
            }
            viewModel.AnswerOneId = answers[0].Id;
            viewModel.OptionOne = answers[0].Value;
            viewModel.AnswerTwoId = answers[1].Id;
            viewModel.OptionTwo = answers[1].Value;
            viewModel.AnswerThreeId = answers[2].Id;
            viewModel.OptionThree = answers[2].Value;

            return viewModel;
        }

        public EditQuestionViewModel GetEditQuestionViewModel(EditQuestionBindingModel model)
        {
            EditQuestionViewModel viewModel = Mapper.Instance
                .Map<EditQuestionBindingModel, EditQuestionViewModel>(model);

            return viewModel;
        }

        public void EditQuestion(EditQuestionBindingModel model)
        {
            Question question = this.GetQuestion(model.Id);
            if (question == null)
            {
                return;
            }
            question.Description = model.Description;

            bool firstCorrect = model.CorrectAnswer == 1;
            bool secondCorrect = model.CorrectAnswer == 2;
            bool thirdCorrect = model.CorrectAnswer == 3;

            Answer answerOne = this.Context.Answers.Find(model.AnswerOneId);
            if (answerOne != null)
            {
                answerOne.Value = model.OptionOne;
                answerOne.IsCorrect = firstCorrect;
            }

            Answer answerTwo = this.Context.Answers.Find(model.AnswerTwoId);
            if (answerTwo != null)
            {
                answerTwo.Value = model.OptionTwo;
                answerTwo.IsCorrect = secondCorrect;
            }

            Answer answerThree = this.Context.Answers.Find(model.AnswerThreeId);
            if (answerThree != null)
            {
                answerThree.Value = model.OptionThree;
                answerThree.IsCorrect = thirdCorrect;
            }

            this.Context.SaveChanges();
        }

        public PreviewQuestionViewModel GetPreviewQuestionViewModel(int id)
        {
            Question question = this.GetQuestion(id);
            if ( question== null)
            {
                return null;
            }
            PreviewQuestionViewModel viewModel = Mapper.Instance
                .Map<Question, PreviewQuestionViewModel>(question);

            viewModel.Answers = Mapper.Instance
                .Map<ICollection<Answer>, ICollection<AnswerViewModel>>(question.Answers);

            return viewModel;
        }

        public void EditQuiz(EditQuizBindingModel model)
        {
            Quiz quiz = this.GetQuiz(model.Id);
            if (quiz== null)
            {
                return;
            }
            quiz.Title = model.Title;

            this.Context.SaveChanges();
        }

        public PreviewQuizViewModel GetPreviewQuizViewModel(int id)
        {
            Quiz quiz = this.GetQuiz(id);
            if (quiz==null)
            {
                return null;
            }
            PreviewQuizViewModel viewModel = Mapper.Instance
                .Map<Quiz, PreviewQuizViewModel>(quiz);

            foreach (Question question in quiz.Questions)
            {
                PreviewQuestionViewModel questionViewModel = this.GetPreviewQuestionViewModel(question.Id);
                viewModel.Questions.Add(questionViewModel);
            }

            return viewModel;
        }

        public void RemoveQuestionFromQuiz(int questionId, int quizId)
        {
            Quiz quiz = this.GetQuiz(quizId);
            Question question = this.GetQuestion(questionId);
            if (quiz != null && question != null)
            {
            quiz.Questions.Remove(question);
            }
            this.Context.SaveChanges();
        }

        public void AddQuestionToQuiz(int questionId, int quizId)
        {
            Quiz quiz = this.GetQuiz(quizId);
            Question question = this.GetQuestion(questionId);
            if (quiz != null && question != null)
            {
                if (quiz.Questions.Count >= 10)
                {
                    throw new ArgumentException("There can only be 10 questions in the quiz!");
                }
                quiz.Questions.Add(question);
            }
           
            this.Context.SaveChanges();
        }

        private Answer AddAnswer(string value, bool isCorrect)
        {
            Answer answer = new Answer()
            {
                Value = value,
                IsCorrect = isCorrect
            };

            this.Context.Answers.Add(answer);
            this.Context.SaveChanges();

            return answer;
        }

        private Question GetQuestion(int id)
        {
            return this.Context.Questions.Find(id);
        }

        private Quiz GetQuiz(int id)
        {
            return this.Context.Quizzes.Find(id);
        }
    }
}
