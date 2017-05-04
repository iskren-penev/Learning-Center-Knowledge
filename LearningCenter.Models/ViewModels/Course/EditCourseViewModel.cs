namespace LearningCenter.Models.ViewModels.Course
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LearningCenter.Models.ViewModels.Admin;

    public class EditCourseViewModel
    {
        public EditCourseViewModel()
        {
            this.UnitsInCourse = new List<UnitListViewModel>();
            this.UnassignedUnits = new List<UnitListViewModel>();
            this.QuizzesInCourse = new List<QuizListViewModel>();
            this.UnassignedQuizzes = new List<QuizListViewModel>();
        }

        public int Id { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and 50 characters long.", MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 50, ErrorMessage = "The {0} must be between {2} and 300  characters long.")]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 250, ErrorMessage = "The {0} must be between {2} and 10000 characters long.")]
        public string Description { get; set; }
        
        [Display(Name = "Show units in course")]
        public ICollection<UnitListViewModel> UnitsInCourse { get; set; }

        [Display(Name = "Show unassigned units")]
        public ICollection<UnitListViewModel> UnassignedUnits { get; set; }

        [Display(Name = "Show quizzes in course")]
        public ICollection<QuizListViewModel> QuizzesInCourse { get; set; }

        [Display(Name = "Show unassigned quizzes")]
        public ICollection<QuizListViewModel> UnassignedQuizzes { get; set; }

    }
}
