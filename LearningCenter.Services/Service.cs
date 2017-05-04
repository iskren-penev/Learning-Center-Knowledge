namespace LearningCenter.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;
    using LearningCenter.Services.Interfaces;

    public abstract class Service : IService
    {
        public Service(ILearningCenterContext context)
        {
            this.Context = context;
        }

        public ILearningCenterContext Context { get; set; }

        public User GetCurrentUser(string userId)
        {
            return this.Context.Users.Find(userId);
        }

        public List<UnitListViewModel> GetAllUnits()
        {
            List<Unit> units = this.Context.Units.ToList();
            var viewModels = Mapper.Instance
                .Map<List<Unit>, List<UnitListViewModel>>(units);

            for (int i = 0; i < viewModels.Count; i++)
            {
                var unit = units.FirstOrDefault(u => u.Id == viewModels[i].Id);
                if (unit.CourseId != null)
                {
                    viewModels[i].CourseName = unit.Course.Title;
                }
                else
                {
                    viewModels[i].CourseName = "Unassigned";
                }
            }

            return viewModels;
        }

        public List<UnitListViewModel> SearchUnits(string search)
        {
            List<UnitListViewModel> viewModels = this.GetAllUnits();

            if (!string.IsNullOrEmpty(search))
            {
                viewModels = viewModels.Where(model =>
                    model.Title.Contains(search)
                    || model.CourseName.Contains(search)).ToList();
            }

            return viewModels;
        }

        public List<QuizListViewModel> GetAllQuizzes()
        {
            List<Quiz> quizzes = this.Context.Quizzes.ToList();
            List<QuizListViewModel> viewModels = Mapper.Instance
                .Map<List<Quiz>, List<QuizListViewModel>>(quizzes);

            for (int i = 0; i < viewModels.Count; i++)
            {
                var quiz = quizzes.FirstOrDefault(u => u.Id == viewModels[i].Id);
                if (quiz.CourseId != null)
                {
                    viewModels[i].CourseName = quiz.Course.Title;
                }
                else
                {
                    viewModels[i].CourseName = "Unassigned";
                }
            }

            return viewModels;
        }

        public List<QuizListViewModel> SearchQuizzes(string search)
        {
            List<QuizListViewModel> viewModels = this.GetAllQuizzes();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model => model.Title.ToLower().Contains(search)
                                                       || model.CourseName.ToLower().Contains(search))
                                                       .ToList();
            }

            return viewModels;
        }

        public List<QuestionListViewModel> GetAllQuestions()
        {
            List<Question> questions = this.Context.Questions.ToList();
            List<QuestionListViewModel> viewModels = Mapper.Instance
                .Map<List<Question>, List<QuestionListViewModel>>(questions);

            for (int i = 0; i < viewModels.Count; i++)
            {
                var question = questions.FirstOrDefault(u => u.Id == viewModels[i].Id);
                if (question.QuizId != null)
                {
                    viewModels[i].QuizName = question.Quiz.Title;
                }
                else
                {
                    viewModels[i].QuizName = "Unassigned";
                }
            }

            return viewModels;
        }

        public List<QuestionListViewModel> SearchQuestions(string search)
        {
            List<QuestionListViewModel> viewModels = this.GetAllQuestions();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model => model.QuizName.ToLower().Contains(search)).ToList();
            }

            return viewModels;
        }
    }
}
