namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeUnitDbSet :FakeDbSet<Unit>
    {
        public override Unit Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(unit => unit.Id == wantedId);
        }
    }
}
