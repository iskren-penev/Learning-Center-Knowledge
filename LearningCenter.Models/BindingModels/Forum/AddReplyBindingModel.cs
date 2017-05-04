namespace LearningCenter.Models.BindingModels.Forum
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AddReplyBindingModel
    {
        [Required]
        [StringLength(10000, MinimumLength = 50, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [AllowHtml]
        public string Content { get; set; }

        public int TopicId { get; set; }
    }
}
