﻿namespace LearningCenter.Models.ViewModels.Course
{
    using System.Collections.Generic;
    using LearningCenter.Models.ViewModels.Quiz;
    using LearningCenter.Models.ViewModels.Units;

    public class DetailedCourseViewModel
    {
        public DetailedCourseViewModel()
        {
            this.Units = new List<UnitsInCourseListViewModel>();
            this.Quizzes = new List<PreviewQuizViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public ICollection<UnitsInCourseListViewModel> Units { get; set; }

        public ICollection<PreviewQuizViewModel> Quizzes { get; set; }


        public int StudentsInCourse { get; set; }

        public bool IsCurrentUserEnrolled  { get; set; }
    }
}
