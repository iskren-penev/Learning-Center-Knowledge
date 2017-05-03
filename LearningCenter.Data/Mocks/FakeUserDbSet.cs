namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeUserDbSet : FakeDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            string wantedId = (string)keyValues[0];
            return this.Set.FirstOrDefault(user => user.Id == wantedId);
        }
    }
}
