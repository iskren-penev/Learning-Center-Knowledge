namespace LearningCenter.Models.ViewModels.Forum
{
    using System.Web.Mvc;

    public class EditTopicViewModel
    {
        public int Id { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Tags { get; set; }
    }
}
