namespace LearningCenter.App.Areas.Forum.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using LearningCenter.Models.BindingModels.Forum;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using PagedList;

    [Authorize]
    [RouteArea("forum")]
    public class ForumController : Controller
    {
        private IForumService service;

        public ForumController(IForumService service)
        {
            this.service = service;
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route]
        public ActionResult All(string parameter, int? pageNumber)
        {
            IEnumerable<AllTopicsViewModel> viewModels = this.service.GetAllTopics(parameter);
            
            return this.View(viewModels.ToPagedList(pageNumber ?? 1, 10));
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public ActionResult Detailed(int id)
        {
            this.ViewBag.TopicId = id;
            DetailedTopicViewModel viewModel = this.service.DetailedTopic(id);
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return this.View(new AddTopicViewModel());
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Title,Content,Category")] AddTopicBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                this.service.AddTopic(model, userId);
                return this.RedirectToAction("All");
            }
            return this.View(this.service.GetAddTopicViewModel(model));

        }

        [HttpGet]
        [Route("edit/{id:int:min(1)}")]
        public ActionResult EditTopic(int id)
        {
            EditTopicViewModel viewModel = this.service.GetEditViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("edit/{id:int:min(1)}")]
        public ActionResult EditTopic([Bind(Include = "Id,Content,Category")] EditTopicBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditTopic(model);
                return this.RedirectToAction("Detailed", new { id = model.Id });
            }

            return this.View(this.service.GetEditViewModel(model.Id));
        }

        
        [HttpGet]
        public ActionResult AddReply()
        {
            return this.PartialView();
        }

        [HttpPost]
        public ActionResult AddReply([Bind(Include = "TopicId,Content")] AddReplyBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                this.service.AddReply(model, userId);

                return this.RedirectToAction("Detailed", new { id = model.TopicId });
            }
            return this.PartialView();
        }
    }
}