﻿namespace LearningCenter.Models.ViewModels.Units
{
    using System.ComponentModel.DataAnnotations;

    public class UnitDetailsViewModel
    {
        public  int Id { get; set; }

        public string Title { get; set; }
        
        public string ContentUrl { get; set; }

        [Display(Name = "Course")]
        public string CourseName { get; set; }

    }
}
