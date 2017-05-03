namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeGradeDbSet : FakeDbSet<Grade>
    {
        public override Grade Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(grade => grade.Id == wantedId);
        }
    }
}
