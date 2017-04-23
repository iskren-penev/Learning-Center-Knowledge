namespace LearningCenter.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class UnitListViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        [Display(Name = "Course")]
        public string CourseName { get; set; }
    }
}
