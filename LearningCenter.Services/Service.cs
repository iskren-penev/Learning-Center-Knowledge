namespace LearningCenter.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Data;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;

    public abstract class Service
    {
        public Service()
        {
            this.Context = new LearningCenterContext();
        }

        protected LearningCenterContext Context { get; set; }

        protected User GetCurrentUser(string userId)
        {
            return this.Context.Users.Find(userId);
        }

        public IEnumerable<UnitListViewModel> GetAllUnits()
        {
            IEnumerable<Unit> units = this.Context.Units;
            var viewModels = Mapper.Instance
                .Map<IEnumerable<Unit>, IEnumerable<UnitListViewModel>>(units).ToArray();

            for (int i = 0; i < viewModels.Length; i++)
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

        public IEnumerable<UnitListViewModel> SearchUnits(string search)
        {
            IEnumerable<UnitListViewModel> viewModels = this.GetAllUnits();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model =>
                    model.Title.ToLower().Contains(search)
                    || model.CourseName.ToLower().Contains(search));
            }

            return viewModels;
        }

        public IEnumerable<QuizListViewModel> GetAllQuizzes()
        {
            IEnumerable<Quiz> quizzes = this.Context.Quizzes;
            QuizListViewModel[] viewModels = Mapper.Instance
                .Map<IEnumerable<Quiz>, IEnumerable<QuizListViewModel>>(quizzes).ToArray();

            for (int i = 0; i < viewModels.Length; i++)
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

        public IEnumerable<QuizListViewModel> SearchQuizzes(string search)
        {
            IEnumerable<QuizListViewModel> viewModels = this.GetAllQuizzes();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model => model.Title.ToLower().Contains(search)
                                                       || model.CourseName.ToLower().Contains(search));
            }

            return viewModels;
        }

        public IEnumerable<QuestionListViewModel> GetAllQuestions()
        {
            IEnumerable<Question> questions = this.Context.Questions;
            QuestionListViewModel[] viewModels = Mapper.Instance
                .Map<IEnumerable<Question>, IEnumerable<QuestionListViewModel>>(questions).ToArray();

            for (int i = 0; i < viewModels.Length; i++)
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

        public IEnumerable<QuestionListViewModel> SearchQuestions(string search)
        {
            IEnumerable<QuestionListViewModel> viewModels = this.GetAllQuestions();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model => model.QuizName.ToLower().Contains(search));
            }

            return viewModels;
        }
    }
}
