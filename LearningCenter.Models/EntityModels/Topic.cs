namespace LearningCenter.Models.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Topic
    {
        private ICollection<Reply> replies;
        private ICollection<Tag> tags;


        public Topic()
        {
            this.replies = new List<Reply>();
            this.tags = new List<Tag>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
        
        public virtual User Author { get; set; }

        public virtual ICollection<Reply> Replies
        {
            get { return this.replies; }
            set { this.replies = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
