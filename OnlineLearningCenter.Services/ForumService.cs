namespace LearningCenter.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Forum;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Services.Interfaces;

    public class ForumService: Service, IForumService
    {

        public void AddNewTopic(AddTopicBindingModel model)
        {
            Topic newTopic = Mapper.Instance.Map<Topic>(model);
            newTopic.PublishDate = DateTime.Now;

            this.Context.Topics.Add(newTopic);
            this.Context.SaveChanges();
        }

        public IEnumerable<AllTopicsViewModel> GetAllTopics()
        {
            IEnumerable<Topic> topics = this.Context.Topics;
            IEnumerable<AllTopicsViewModel> viewModels = Mapper.Instance.Map<IEnumerable<AllTopicsViewModel>>(topics);
            return viewModels;
        }

        public DetailedTopicViewModel DetailedTopic(int id)
        {
            Topic topic = this.Context.Topics.Find(id);
            DetailedTopicViewModel viewModel = Mapper.Instance.Map<DetailedTopicViewModel>(topic);
            viewModel.Replies = Mapper.Map<IEnumerable<ReplyViewModel>>(topic.Replies);
            return viewModel;
        }

        public void AddTopic(AddTopicBindingModel model, string userId)
        {
            User currentUser = this.Context.Users.Find(userId);
            Category category = this.Context.Categories.FirstOrDefault(cat => cat.Name == model.Category);
            if (category == null)
            {
                category = new Category()
                {
                    Name = model.Category
                };
                this.Context.Categories.Add(category);
                this.Context.SaveChanges();
            }
            Topic topic = Mapper.Instance.Map<Topic>(model);
            topic.Author = currentUser;
            topic.PublishDate = DateTime.Now;
            topic.Category = category;

            this.Context.Topics.Add(topic);
            this.Context.SaveChanges();

        }

        public EditTopicViewModel GetEditViewModel(int id)
        {
            Topic topic = this.Context.Topics.Find(id);
            EditTopicViewModel viewModel = Mapper.Instance.Map<EditTopicViewModel>(topic);
            return viewModel;
        }

        public void EditTopic(EditTopicBindingModel model)
        {
            Topic topic = this.Context.Topics.Find(model.Id);
            topic.Content = model.Content;
            this.Context.SaveChanges();
        }

        public void AddReply(AddReplyBindingModel model, string userId)
        {
            User currentUser = this.GetCurrentUser(userId);
            Topic currentTopic = this.Context.Topics.Find(model.TopicId);
            Reply reply = Mapper.Instance.Map<Reply>(model);
            reply.Topic = currentTopic;
            reply.Author = currentUser;
            reply.PublishDate = DateTime.Now;

            this.Context.Replies.Add(reply);
            this.Context.SaveChanges();
        }

        public void DeleteTopic()
        {
            throw new NotImplementedException();
        }

        public AddTopicViewModel GetAddTopicViewModel(AddTopicBindingModel model)
        {
            AddTopicViewModel viewModel = Mapper.Instance.Map<AddTopicViewModel>(model);
            return viewModel;
        }

        
    }
}
