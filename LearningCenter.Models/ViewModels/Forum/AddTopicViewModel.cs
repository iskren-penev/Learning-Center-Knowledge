namespace LearningCenter.Models.ViewModels.Forum
{
    using System.Web.Mvc;

    public class AddTopicViewModel
    {
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Category { get; set; }
    }
}
