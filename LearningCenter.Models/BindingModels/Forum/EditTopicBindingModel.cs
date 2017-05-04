namespace LearningCenter.Models.BindingModels.Forum
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EditTopicBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 50, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        public string Tags { get; set; }
    }
}
