namespace LearningCenter.Models.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LearningCenter.Models.ViewModels.Course;
    using LearningCenter.Models.ViewModels.Forum;

    public class ProfileViewModel
    {
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname(s)")]
        public string LastName { get; set; }

        [Display(Name = "Enrolled courses")]
        public IEnumerable<AllCourseViewModel> EnrolledCourses { get; set; }
        
        [Display(Name = "Forum topics")]
        public IEnumerable<AllTopicsViewModel> ForumTopics { get; set; }

        [Display(Name = "Quiz results")]
        public IEnumerable<GradeViewModel> QuizResults { get; set; }
    }
}
