﻿namespace LearningCenter.App
{
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Models.BindingModels.Courses;
    using LearningCenter.Models.BindingModels.Forum;
    using LearningCenter.Models.BindingModels.Quiz;
    using LearningCenter.Models.BindingModels.Units;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Forum;
    using LearningCenter.Models.ViewModels.Quiz;
    using LearningCenter.Models.ViewModels.Units;
    using LearningCenter.Models.ViewModels.User;

    public static class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(exp =>
            {
                #region Forum mappings

                exp.CreateMap<AddTopicBindingModel, Topic>()
                .ForMember(model => model.Tags, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<Topic, AllTopicsViewModel>()
                    .ForMember(model => model.Author,
                        configurationExpression =>
                            configurationExpression.MapFrom(topic => $"{topic.Author.FirstName} {topic.Author.LastName}"))
                    .ForMember(model => model.Replies,
                        configurationExpression => configurationExpression.MapFrom(topic => topic.Replies.Count))
                    .ForMember(model => model.Tags, configurationExpression =>
                        configurationExpression.MapFrom(topic => topic.Tags.Select(t => t.Name)));

                exp.CreateMap<Topic, EditTopicViewModel>()
                    .ForMember(model => model.Tags, configurationExpression =>
                        configurationExpression.MapFrom(topic => string.Join(",", topic.Tags.Select(t => t.Name))));

                exp.CreateMap<Reply, ReplyViewModel>()
                    .ForMember(model => model.Author, configurationExpression =>
                        configurationExpression.MapFrom(reply => $"{reply.Author.FirstName} {reply.Author.LastName}"));

                exp.CreateMap<Topic, DetailedTopicViewModel>()
                    .ForMember(model => model.Author,
                        configurationExpression =>
                            configurationExpression.MapFrom(topic => $"{topic.Author.FirstName} {topic.Author.LastName}"))
                    .ForMember(model => model.AuthorEmail,
                        configurationExpression =>
                            configurationExpression.MapFrom(topic => topic.Author.Email))
                    .ForMember(model => model.Replies, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.Tags, configurationExpression =>
                        configurationExpression.MapFrom(topic => topic.Tags.Select(t => t.Name))); 

                exp.CreateMap<AddTopicBindingModel, AddTopicViewModel>();

                exp.CreateMap<AddReplyBindingModel, Reply>()
                    .ForMember(model => model.TopicId, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<Tag, ForumTagViewModel>()
                    .ForMember(model => model.TopicsCount, configurationExpression =>
                        configurationExpression.MapFrom(tag => tag.Topics.Count));

                #endregion

                #region Courses mappings
                exp.CreateMap<Course, AllCourseViewModel>()
                    .ForMember(model => model.StudentsInCourse,
                        configurationExpression => configurationExpression.MapFrom(course => course.Students.Count));

                exp.CreateMap<Course, CourseListViewModel>()
                   .ForMember(model => model.Students, configurationExpression =>
                       configurationExpression.MapFrom(course => course.Students.Count));

                exp.CreateMap<AddCourseBindingModel, Course>();

                exp.CreateMap<AddCourseBindingModel, AddCourseViewModel>();

                exp.CreateMap<Course, EditCourseViewModel>()
                    .ForMember(model => model.UnitsInCourse, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.UnassignedUnits, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.QuizzesInCourse, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.UnassignedQuizzes, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<Course, DetailedCourseViewModel>()
                    .ForMember(model => model.StudentsInCourse, configurationExpression =>
                        configurationExpression.MapFrom(course => course.Students.Count))
                    .ForMember(model => model.IsCurrentUserEnrolled, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.Units, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<Grade, GradeViewModel>()
                    .ForMember(model => model.CourseId, configurationExpression =>
                        configurationExpression.MapFrom(grade => grade.Course.Id))
                    .ForMember(model => model.CourseTitle, configurationExpression =>
                        configurationExpression.MapFrom(grade => grade.Course.Title));

                #endregion

                #region Units Mappings

                exp.CreateMap<Unit, UnitDetailsViewModel>()
                    .ForMember(model => model.CourseName, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<AddUnitBindingModel, Unit>();

                exp.CreateMap<AddUnitBindingModel, AddUnitViewModel>();

                exp.CreateMap<Unit, EditUnitViewModel>();

                exp.CreateMap<Unit, UnitsInCourseListViewModel>();

                #endregion

                #region Quiz mappings

                exp.CreateMap<Quiz, QuizListViewModel>()
                    .ForMember(model => model.CourseName, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<AddQuizBindingModel, Quiz>();

                exp.CreateMap<AddQuizBindingModel, AddQuizViewModel>();

                exp.CreateMap<Quiz, EditQuizViewModel>()
                    .ForMember(model => model.NumberOfQuestionsInQuiz, configurationExpression =>
                        configurationExpression.MapFrom(quiz => quiz.Questions.Count))
                    .ForMember(model => model.QuestionsInQuiz, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.UnassignedQuestions, configurationExpression =>
                        configurationExpression.Ignore());

                #endregion

                #region Question mappings

                exp.CreateMap<Question, QuestionListViewModel>()
                    .ForMember(model => model.QuizName, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<AddQuestionBindingModel, Question>();

                exp.CreateMap<AddQuestionBindingModel, AddQuestionViewModel>();

                exp.CreateMap<EditQuestionBindingModel, EditQuestionViewModel>();

                exp.CreateMap<Answer, AnswerViewModel>();

                exp.CreateMap<Question, PreviewQuestionViewModel>()
                    .ForMember(model => model.Answers, configurationExpression =>
                        configurationExpression.Ignore());

                exp.CreateMap<Quiz, PreviewQuizViewModel>()
                    .ForMember(model => model.Questions, configurationExpression =>
                        configurationExpression.Ignore());

                #endregion

                #region User profile mappings

                exp.CreateMap<User, EditProfileViewModel>();

                exp.CreateMap<User, ProfileViewModel>()
                    .ForMember(model => model.EnrolledCourses, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.ForumTopics, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.QuizResults, configurationExpression =>
                        configurationExpression.Ignore());
                #endregion

                #region AdminPanel mappings

                //users
                exp.CreateMap<User, UserListViewModel>()
                    .ForMember(model => model.Roles, configurationExpression =>
                        configurationExpression.Ignore())
                    .ForMember(model => model.EnrolledCourses, configurationExpression =>
                        configurationExpression.MapFrom(user => user.EnrolledCourses.Count));


                exp.CreateMap<Unit, UnitListViewModel>()
                    .ForMember(model => model.CourseName, configurationExpression =>
                        configurationExpression.Ignore());



                #endregion

            });
        }
    }
}