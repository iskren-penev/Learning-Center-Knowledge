namespace LearningCenter.Models.ViewModels.Course
{
    using System.Collections.Generic;
    using LearningCenter.Models.ViewModels.Admin;

    public class EditCourseViewModel
    {
        public EditCourseViewModel()
        {
            this.UnitsInCourse = new List<UnitListViewModel>();
            this.UnassignedUnits = new List<UnitListViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        
        public ICollection<UnitListViewModel> UnitsInCourse { get; set; }

        public ICollection<UnitListViewModel> UnassignedUnits { get; set; }

    }
}
