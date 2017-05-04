namespace LearningCenter.Tests.Controllers
{
    using System.Net;
    using LearningCenter.App;
    using LearningCenter.App.Areas.Admin.Controllers;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Data.Mocks;
    using LearningCenter.Models.BindingModels.Quiz;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Quiz;
    using LearningCenter.Services.Implementations;
    using LearningCenter.Services.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class TestQuizzesController
    {
        private QuizzesController controller;
        private IQuizService service;
        private ILearningCenterContext context;

        [TestInitialize]
        public void Initialize()
        {
            AutomapperConfig.RegisterMappings();
            this.context = new FakeLearningCenterContext();
            this.service = new QuizService(this.context);
            this.controller = new QuizzesController(this.service);

            Course course = new Course()
            {
                Id = 1,
                Description = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                ShortDescription = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
                Title = "TestCourse"
            };
            Unit unit = new Unit()
            {
                Id = 1,
                Title = "Test",
                ContentUrl = "someContent"
            };

            Quiz quiz = new Quiz()
            {
                Course = course,
                Id = 1,
                Title = "TestQuiz"
            };
            Grade grade = new Grade()
            {
                Id = 1,
                Course = course,
                CourseId = 1,
                QuizTitle = "QuizTitle",
                Result = 5
            };
            Answer one = new Answer() {Id = 1, IsCorrect = false, Value = "text1"};
            Answer two = new Answer() { Id = 2, IsCorrect = false, Value = "text2" };
            Answer three = new Answer() { Id = 3, IsCorrect = false, Value = "text3" };
            Question ques = new Question() {Id = 1, Quiz = quiz, Description = "Lorem ipsum Lorem ipsum Lorem ipsum ", QuizId = quiz.Id};
            ques.Answers.Add(one);
            ques.Answers.Add(two);
            ques.Answers.Add(three);

            this.context.Answers.Add(one);
            this.context.Answers.Add(two);
            this.context.Answers.Add(three);
            this.context.Questions.Add(ques);
            this.context.Courses.Add(course);
            this.context.Units.Add(unit);
            this.context.Quizzes.Add(quiz);
            this.context.Grades.Add(grade);
        }

        [TestMethod]
        public void AddQuiz_Get_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.AddQuiz())
                .ShouldRenderDefaultView()
                .WithModel<AddQuizViewModel>();
        }

        [TestMethod]
        public void AddQuiz_Post_RedirectToQuizList()
        {
            this.controller.WithCallTo(c => c.AddQuiz(new AddQuizBindingModel() {Title = "SomeTitle"}))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("QuizList"));
        }

        [TestMethod]
        public void EditQuiz_Get_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.EditQuiz(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
        
        [TestMethod]
        public void EditQuiz_Get_Valid_ReturnDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.EditQuiz(1))
                .ShouldRenderDefaultView()
                .WithModel<EditQuizViewModel>();
        }

        [TestMethod]
        public void EditQuiz_Post_RedirectToPreviewQuiz()
        {
            EditQuizBindingModel model = new EditQuizBindingModel()
            {
                Id = 1, Title = "ChangedTitle"
            };
            this.controller.WithCallTo(c => c.EditQuiz(model))
                .ShouldRedirectTo(c => c.PreviewQuiz(1));
        }

        [TestMethod]
        public void PreviewQuiz_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.PreviewQuiz(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
        
        [TestMethod]
        public void PreviewQuiz_Valid_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.PreviewQuiz(1))
                .ShouldRenderDefaultView()
                .WithModel<PreviewQuizViewModel>();
        }

        [TestMethod]
        public void AddQuestion_Get_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.AddQuestion())
                .ShouldRenderDefaultView()
                .WithModel<AddQuestionViewModel>();
        }

        [TestMethod]
        public void AddQuestion_Post_RedirectToQuestionList()
        {
            AddQuestionBindingModel model = new AddQuestionBindingModel()
            {
                CorrectAnswer = 1, Description = "Lorem ipsum ...", OptionTwo = "text2", OptionOne = "text1", OptionThree = "text3"
            };
            this.controller.WithCallTo(c => c.AddQuestion(model))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("QuestionList"));
        }

        [TestMethod]
        public void EditQuestion_Get_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.EditQuestion(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
        
        [TestMethod]
        public void EditQuestion_Get_Valid_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.EditQuestion(1))
                .ShouldRenderDefaultView()
                .WithModel<EditQuestionViewModel>();
        }

        [TestMethod]
        public void EditQuestion_Post_RedirectToPreviewQuestion()
        {
            EditQuestionBindingModel model = new EditQuestionBindingModel()
            {
                CorrectAnswer = 1, Id = 1, AnswerOneId = 1, AnswerTwoId = 2, AnswerThreeId = 3,
                Description = "lipsum lipsum, lipsum", OptionOne = "text1", OptionTwo = "text2", OptionThree = "exit"
            };
            this.controller.WithCallTo(c => c.EditQuestion(model))
                .ShouldRedirectTo(c => c.PreviewQuestion(1));
        }

        [TestMethod]
        public void PreviewQuestion_InvalidId_BadRequest()
        {
            this.controller.WithCallTo(c => c.PreviewQuestion(-1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
        
        [TestMethod]
        public void PreviewQuestion_Valid_RenderDefaultViewWithModel()
        {
            this.controller.WithCallTo(c => c.PreviewQuestion(1))
                .ShouldRenderDefaultView()
                .WithModel<PreviewQuestionViewModel>();
        }

        [TestMethod]
        public void AddToQuiz_InvalidIds_BadRequest()
        {
            this.controller.WithCallTo(c => c.AddToQuiz(-1, -1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void AddToQuiz_Valid_RedirectToEditQuiz()
        {
            this.controller.WithCallTo(c => c.AddToQuiz(1, 1))
                .ShouldRedirectTo(c => c.EditQuiz(1));
        }

        [TestMethod]
        public void RemoveFromQuiz_InvalidIds_BadRequest()
        {
            this.controller.WithCallTo(c => c.RemoveFromQuiz(-1, -1))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void RemoveFromQuiz_Valid_RedirectToEditQuiz()
        {
            this.controller.WithCallTo(c => c.RemoveFromQuiz(1, 1))
                .ShouldRedirectTo(c => c.EditQuiz(1));
        }
    }
}
