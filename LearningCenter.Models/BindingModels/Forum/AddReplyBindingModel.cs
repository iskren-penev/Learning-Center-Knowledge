namespace LearningCenter.Models.BindingModels.Forum
{
    using System.Web.Mvc;

    public class AddReplyBindingModel
    {
        [AllowHtml]
        public string Content { get; set; }

        public int TopicId { get; set; }
    }
}
