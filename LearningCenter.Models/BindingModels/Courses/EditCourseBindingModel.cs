namespace LearningCenter.Models.BindingModels.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LearningCenter.Models.ViewModels.Admin;

    public class EditCourseBindingModel
    {
        public EditCourseBindingModel()
        {
            this.UnitsInCourse = new List<UnitListViewModel>();
            this.UnassignedUnits = new List<UnitListViewModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string ShortDescription { get; set; }

        [Required]
        [MinLength(250)]
        public string Description { get; set; }
        
        public ICollection<UnitListViewModel> UnitsInCourse { get; set; }

        public ICollection<UnitListViewModel> UnassignedUnits { get; set; }
    }
}
