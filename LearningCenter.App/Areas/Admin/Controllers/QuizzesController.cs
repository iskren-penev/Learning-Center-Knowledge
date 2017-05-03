namespace LearningCenter.App.Areas.Admin.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.Quiz;
    using LearningCenter.Models.ViewModels.Quiz;
    using LearningCenter.Services.Interfaces;

    [Authorize(Roles = "Admin,Instructor")]
    [RouteArea("admin")]
    public class QuizzesController : Controller
    {
        private IQuizService service;

        public QuizzesController(IQuizService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("quizzes/add")]
        public ActionResult AddQuiz()
        {
            return this.View(new AddQuizViewModel());
        }

        [HttpPost]
        [Route("quizzes/add")]
        public ActionResult AddQuiz([Bind(Include = "Title")] AddQuizBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddNewQuiz(model);
                return this.RedirectToAction("QuizList", "Admin", new {area = "Admin"});
            }
            AddQuizViewModel viewModel = this.service.GetAddViewModel(model);

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("quizzes/edit/{id:int:min(1)}")]
        public ActionResult EditQuiz(int id)
        {
            EditQuizViewModel viewModel = this.service.GetEditViewModel(id);
            if (viewModel==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("quizzes/edit/{id:int:min(1)}")]
        public ActionResult EditQuiz([Bind(Include = "Id,Title")] EditQuizBindingModel model)
        {
            if (ModelState.IsValid)
            {
                this.service.EditQuiz(model);
                return this.RedirectToAction("PreviewQuiz", "Quizzes", new { area = "Admin", id=model.Id });
            }
            EditQuizViewModel viewModel = this.service.GetEditViewModel(model.Id);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("quizzes/{id:int:min(1)}")]
        public ActionResult PreviewQuiz(int id)
        {
            PreviewQuizViewModel viewModel = this.service.GetPreviewQuizViewModel(id);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [Route("quizzes/addtoquiz")]
        public ActionResult AddToQuiz(int questionId, int quizId)
        {
            if (questionId < 1 || quizId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            this.service.AddQuestionToQuiz(questionId, quizId);
            return this.RedirectToAction("EditQuiz", new {area = "Admin", id = quizId});
        }

        [Route("quizzes/removequiz")]
        public ActionResult RemoveFromQuiz(int questionId, int quizId)
        {
            if (questionId < 1 || quizId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            this.service.RemoveQuestionFromQuiz(questionId, quizId);
            return this.RedirectToAction("EditQuiz", new { area = "Admin", id = quizId });
        }

        [HttpGet]
        [Route("questions/add")]
        public ActionResult AddQuestion()
        {
            return this.View(new AddQuestionViewModel());
        }

        [HttpPost]
        [Route("questions/add")]
        public ActionResult AddQuestion([Bind(Include = "Description,OptionOne,OptionTwo,OptrionThree,CorrectAnswer")]
            AddQuestionBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddQuestion(model);
                return this.RedirectToAction("QuestionList", "Admin", new { area = "Admin" });
            }
            return this.View(new AddQuestionViewModel());
        }

        [HttpGet]
        [Route("questions/edit/{id:int:min(1)}")]
        public ActionResult EditQuestion(int id)
        {
            EditQuestionViewModel viewModel = this.service.GetEditQuestionViewModel(id);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("questions/edit/{id:int:min(1)}")]
        public ActionResult EditQuestion(
            [Bind(Include = "Id,Description,OptionOne,OptionTwo,OptionThree,CorrectAnswer,AnswerOneId,AnswerTwoId,AnswerThreeId")]
            EditQuestionBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditQuestion(model);
                return this.RedirectToAction("PreviewQuestion", "Quizzes", new { area = "Admin", id=model.Id });

            }
            EditQuestionViewModel viewModel = this.service.GetEditQuestionViewModel(model);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("questions/{id:int:min(1)}")]
        public ActionResult PreviewQuestion(int id)
        {
            PreviewQuestionViewModel viewModel = this.service.GetPreviewQuestionViewModel(id);
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return this.View(viewModel);
        }
    }
}
