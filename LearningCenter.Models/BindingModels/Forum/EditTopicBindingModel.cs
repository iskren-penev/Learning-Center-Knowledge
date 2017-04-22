namespace LearningCenter.Models.BindingModels.Forum
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EditTopicBindingModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(50)]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
