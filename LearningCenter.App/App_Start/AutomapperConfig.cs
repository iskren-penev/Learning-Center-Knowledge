namespace LearningCenter.App
{
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Forum;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Models.ViewModels.User;

    public static class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(exp =>
            {
                //forum mappings
                exp.CreateMap<AddTopicBindingModel, Topic>()
                .ForMember(model => model.Category, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<Topic, AllTopicsViewModel>()
                    .ForMember(model => model.Author,
                        configurationExpression =>
                            configurationExpression.MapFrom(topic => $"{topic.Author.FirstName} {topic.Author.LastName}"))
                    .ForMember(model => model.Replies,
                        configurationExpression => configurationExpression.MapFrom(topic => topic.Replies.Count))
                    .ForMember(model => model.Category, configurationExpression =>
                        configurationExpression.MapFrom(topic => topic.Category.Name));

                exp.CreateMap<Topic, EditTopicViewModel>()
                    .ForMember(model => model.Category, configurationExpression =>
                        configurationExpression.MapFrom(topic => topic.Category.Name));

                exp.CreateMap<Reply, ReplyViewModel>()
                    .ForMember(model => model.Author, configurationExpression =>
                        configurationExpression.MapFrom(reply => $"{reply.Author.FirstName} {reply.Author.LastName}"));

                exp.CreateMap<Topic, DetailedTopicViewModel>()
                    .ForMember(model => model.Author,
                        configurationExpression =>
                            configurationExpression.MapFrom(topic => $"{topic.Author.FirstName} {topic.Author.LastName}"))
                    .ForMember(model => model.Replies, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.Category, configurationExpression =>
                        configurationExpression.MapFrom(topic => topic.Category.Name)); ;

                exp.CreateMap<AddTopicBindingModel, AddTopicViewModel>();

                exp.CreateMap<AddReplyBindingModel, Reply>()
                    .ForMember(model => model.TopicId, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<Course, AllCourseViewModel>()
                    .ForMember(model => model.InstructorName,
                        configurationExpression =>
                            configurationExpression.MapFrom(
                                course => $"{course.Instructor.FirstName} {course.Instructor.LastName}"))
                    .ForMember(model => model.StudentsInCourse,
                        configurationExpression => configurationExpression.MapFrom(course => course.Students.Count));

                exp.CreateMap<User, EditProfileViewModel>();
                exp.CreateMap<User, ProfileViewModel>();
            });
        }
    }
}