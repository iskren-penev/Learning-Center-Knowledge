namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeQuizDbSet : FakeDbSet<Quiz>
    {
        public override Quiz Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(quiz => quiz.Id == wantedId);
        }
    }
}
