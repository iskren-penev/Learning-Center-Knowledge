namespace LearningCenter.Data
{
    using System.Data.Entity;
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Data.Migrations;
    using LearningCenter.Models.EntityModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class LearningCenterContext : IdentityDbContext<User>, ILearningCenterContext
    {
        public LearningCenterContext()
            : base("OnlineLearningCenter")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LearningCenterContext, Configuration>());
        }

        public static LearningCenterContext Create()
        {
            return new LearningCenterContext();
            
        }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Course>(u => u.EnrolledCourses)
                .WithMany(c => c.Students)
                .Map(configuration =>
                {
                    configuration.MapLeftKey("UserId");
                    configuration.MapRightKey("CourseId");
                    configuration.ToTable("UserCourses");
                });

            modelBuilder.Entity<Topic>()
                .HasMany<Tag>(topic => topic.Tags)
                .WithMany(tag => tag.Topics)
                .Map(configuration =>
                {
                    configuration.MapLeftKey("TopicId");
                    configuration.MapRightKey("TagId");
                    configuration.ToTable("TopicTags");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}