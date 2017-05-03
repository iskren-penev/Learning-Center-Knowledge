namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeReplyDbSet :FakeDbSet<Reply>
    {
        public override Reply Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(rep => rep.Id == wantedId);
        }
    }
}
