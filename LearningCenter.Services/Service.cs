namespace LearningCenter.Services
{
    using System.Linq;
    using LearningCenter.Data;
    using LearningCenter.Models.EntityModels;

    public abstract class Service
    {
        public Service()
        {
            this.Context = new LearningCenterContext();
        }

        protected LearningCenterContext Context { get; set; }

        protected User GetCurrentUser(string userId)
        {
            return this.Context.Users.Find(userId);
        }
        
    }
}
