namespace LearningCenter.Models.ViewModels.Course
{
    public class GradeViewModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int Result { get; set; }

        public string CourseTitle { get; set; }

        public string QuizTitle { get; set; }
    }
}
