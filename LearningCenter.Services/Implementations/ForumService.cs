namespace LearningCenter.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Forum;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Services.Interfaces;

    public class ForumService : Service, IForumService
    {
        public IEnumerable<AllTopicsViewModel> GetAllTopics()
        {
            IEnumerable<Topic> topics = this.Context.Topics;
            IEnumerable<AllTopicsViewModel> viewModels = Mapper.Instance.Map<IEnumerable<AllTopicsViewModel>>(topics);
            return viewModels;
        }


        public IEnumerable<AllTopicsViewModel> SearchTopics(string search)
        {

            IEnumerable<Topic> topics = this.Context.Topics.Where(topic =>
                topic.Title.StartsWith(search)
                || topic.Tags.Any(tag => tag.Name == search)
                || topic.Author.FirstName.Contains(search)
                || topic.Author.LastName.Contains(search));
            IEnumerable<AllTopicsViewModel> viewModels = Mapper.Instance.Map<IEnumerable<AllTopicsViewModel>>(topics);

            return viewModels;
        }

        public DetailedTopicViewModel DetailedTopic(int id)
        {
            Topic topic = this.Context.Topics.Find(id);
            if (topic == null)
            {
                return null;
            }
            DetailedTopicViewModel viewModel = Mapper.Instance.Map<DetailedTopicViewModel>(topic);
            viewModel.Replies = Mapper.Map<IEnumerable<ReplyViewModel>>(topic.Replies);
            return viewModel;
        }

        public void AddTopic(AddTopicBindingModel model, string userId)
        {
            User currentUser = GetCurrentUser(userId);

            Topic topic = Mapper.Instance.Map<Topic>(model);
            topic.Author = currentUser;
            topic.PublishDate = DateTime.Now;

            string[] tags = model.Tags.Split(new[] { ",", ";", " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tagString in tags)
            {
                Tag tag = this.Context.Tags.FirstOrDefault(t => t.Name == tagString);
                if (tag == null)
                {
                    tag = new Tag()
                    {
                        Name = tagString
                    };
                    this.Context.Tags.Add(tag);
                    this.Context.SaveChanges();
                }

                topic.Tags.Add(tag);
            }

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
            if (topic==null)
            {
                return;
            }
            string[] tags = model.Tags.Split(new[] { ",", ";", " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tagString in tags)
            {
                if (!topic.Tags.Any(t => t.Name == tagString))
                {
                    Tag tag = this.Context.Tags.FirstOrDefault(t => t.Name == tagString);
                    if (tag == null)
                    {
                        tag = new Tag()
                        {
                            Name = tagString
                        };
                        this.Context.Tags.Add(tag);
                        this.Context.SaveChanges();
                    }
                    topic.Tags.Add(tag);

                }
            }
            topic.Content = model.Content;
            this.Context.SaveChanges();
        }

        public void AddReply(AddReplyBindingModel model, string userId)
        {
            User currentUser = this.GetCurrentUser(userId);
            Topic currentTopic = this.Context.Topics.Find(model.TopicId);
            if (currentTopic == null)
            {
                return;
            }
            Reply reply = Mapper.Instance.Map<Reply>(model);
            reply.Topic = currentTopic;
            reply.Author = currentUser;
            reply.PublishDate = DateTime.Now;

            this.Context.Replies.Add(reply);
            this.Context.SaveChanges();
        }


        public AddTopicViewModel GetAddTopicViewModel(AddTopicBindingModel model)
        {
            if (model== null)
            {
                return null;
            }
            AddTopicViewModel viewModel = Mapper.Instance.Map<AddTopicViewModel>(model);
            return viewModel;
        }

        public ForumListViewModel GetForumListViewModel()
        {
            ForumListViewModel viewModel = new ForumListViewModel()
            {
                Tags = this.GetTopTags(),
                Topics = this.GetAllTopics()
            };

            return viewModel;
        }

        private IEnumerable<ForumTagViewModel> GetTopTags()
        {
            IEnumerable<Tag> tags = this.Context.Tags
                .OrderByDescending(t => t.Topics.Count)
                .Take(10);
            IEnumerable<ForumTagViewModel> viewModels = Mapper.Instance
                .Map<IEnumerable<Tag>, IEnumerable<ForumTagViewModel>>(tags);

            return viewModels;
        }
    }
}
