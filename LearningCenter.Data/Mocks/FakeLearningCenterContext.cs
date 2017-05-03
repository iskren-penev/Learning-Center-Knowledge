namespace LearningCenter.Data.Mocks
{
    using System.Data.Entity;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Models.EntityModels;

    public class FakeLearningCenterContext : ILearningCenterContext
    {
        public FakeLearningCenterContext()
        {
            this.Topics = new FakeTopicDbSet();
            this.Units = new FakeUnitDbSet();
            this.Answers = new FakeAnswerDbSet();
            this.Courses = new FakeCourseDbSet();
            this.Grades = new FakeGradeDbSet();
            this.Replies = new FakeReplyDbSet();
            this.Quizzes = new FakeQuizDbSet();
            this.Questions = new FakeQuestionDbSet();
            this.Tags = new FakeTagDbSet();
            this.Users = new FakeUserDbSet();
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public IDbSet<User> Users { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
