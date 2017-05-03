namespace LearningCenter.Data.Mocks
{
    using System.Linq;
    using LearningCenter.Models.EntityModels;

    public class FakeCourseDbSet : FakeDbSet<Course>
    {
        public override Course Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(course => course.Id == wantedId);
        }
    }
}
