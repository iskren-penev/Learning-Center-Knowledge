namespace LearningCenter.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 50, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [AllowHtml]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        
        public virtual User Author { get; set; }

        [ForeignKey("Topic")]
        public int? TopicId { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
