namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeQuestionDbSet : FakeDbSet<Question>
    {
        public override Question Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(ques => ques.Id == wantedId);
        }
    }
}
