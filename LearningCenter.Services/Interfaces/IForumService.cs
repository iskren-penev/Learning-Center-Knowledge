﻿namespace LearningCenter.Services.Interfaces
{
    using System.Collections.Generic;
    using LearningCenter.Models.BindingModels.Forum;
    using LearningCenter.Models.ViewModels.Forum;

    public interface IForumService : IService
    {
        IEnumerable<AllTopicsViewModel> GetAllTopics();

        IEnumerable<AllTopicsViewModel> SearchTopics(string search);

        void AddTopic(AddTopicBindingModel model, string userId);

        DetailedTopicViewModel DetailedTopic(int id);

        EditTopicViewModel GetEditViewModel(int id);

        void EditTopic(EditTopicBindingModel model);

        void AddReply(AddReplyBindingModel model, string userId);
        
        AddTopicViewModel GetAddTopicViewModel(AddTopicBindingModel model);

        ForumListViewModel GetForumListViewModel();
    }
}
