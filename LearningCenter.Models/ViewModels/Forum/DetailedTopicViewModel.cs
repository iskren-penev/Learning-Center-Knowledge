namespace LearningCenter.Models.ViewModels.Forum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DetailedTopicViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string AuthorEmail { get; set; }

        [Display(Name = "Published on")]
        public DateTime PublishDate { get; set; }

        public string Category { get; set; }

        public IEnumerable<ReplyViewModel> Replies { get; set; }
    }
}
