namespace LearningCenter.Models.ViewModels.Forum
{
    using System;
    using System.Collections.Generic;

    public class AllTopicsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Replies { get; set; }

        public DateTime PublishDate { get; set; }

        public List<string> Tags { get; set; }
    }
}
