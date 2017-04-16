namespace LearningCenter.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Reply
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        
        public virtual User Author { get; set; }

        [ForeignKey("Topic")]
        public int? TopicId { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
