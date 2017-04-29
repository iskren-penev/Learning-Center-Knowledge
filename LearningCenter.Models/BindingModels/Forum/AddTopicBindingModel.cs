namespace LearningCenter.Models.BindingModels.Forum
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AddTopicBindingModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        [AllowHtml]
        public string Content { get; set; }
        
        [Required]
        public string Tags { get; set; }
    }
}
