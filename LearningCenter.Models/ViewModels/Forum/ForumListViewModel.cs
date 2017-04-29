namespace LearningCenter.Models.ViewModels.Forum
{
    using System.Collections.Generic;

    public class ForumListViewModel
    {
        public IEnumerable<ForumTagViewModel> Tags { get; set; }

        public IEnumerable<AllTopicsViewModel> Topics { get; set; }
    }
}
