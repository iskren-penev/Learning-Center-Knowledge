namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeAnswerDbSet : FakeDbSet<Answer>
    {
        public override Answer Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(ans => ans.Id == wantedId);
        }
    }
}
