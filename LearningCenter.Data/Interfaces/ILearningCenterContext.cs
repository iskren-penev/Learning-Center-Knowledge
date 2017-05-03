namespace LearningCenter.Data.Interfaces
{
    using System.Data.Entity;
    using LearningCenter.Models.EntityModels;

    public interface ILearningCenterContext
    {
        DbSet<Topic> Topics { get; }

        DbSet<Unit> Units { get;  }

        DbSet<Reply> Replies { get;  }

        DbSet<Course> Courses { get; }

        DbSet<Quiz> Quizzes { get;  }

        DbSet<Question> Questions { get; }

        DbSet<Answer> Answers { get;  }

        DbSet<Grade> Grades { get;  }

        DbSet<Tag> Tags { get; }

        IDbSet<User> Users { get; }
        
        int SaveChanges();

    }
}
