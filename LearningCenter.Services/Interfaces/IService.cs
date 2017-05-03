namespace LearningCenter.Services.Interfaces
{
    using LearningCenter.Data.Interfaces;
    using LearningCenter.Models.EntityModels;

    public interface IService
    {
        ILearningCenterContext Context { get; set; }

        User GetCurrentUser(string userId);
    }
}