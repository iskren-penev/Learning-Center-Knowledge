namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeTagDbSet : FakeDbSet<Tag>
    {
        public override Tag Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(tag => tag.Id == wantedId);
        }
    }
}
