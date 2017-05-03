namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeTopicDbSet : FakeDbSet<Topic>
    {
        public override Topic Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(topic => topic.Id == wantedId);
        }
    }
}
