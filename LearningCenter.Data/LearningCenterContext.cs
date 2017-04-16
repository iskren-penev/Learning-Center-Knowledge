namespace LearningCenter.Data
{
    using System.Data.Entity;
    using LearningCenter.Models.EntityModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class LearningCenterContext : IdentityDbContext<User>
    {
        public LearningCenterContext()
            : base("OnlineLearningCenter", throwIfV1Schema: false)
        {
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
        public virtual DbSet<Category> Categories { get; set; }


    }
}