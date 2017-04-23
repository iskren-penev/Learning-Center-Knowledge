namespace LearningCenter.Models.ViewModels.Admin
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserListViewModel
    {

        public UserListViewModel()
        {
            this.Roles = new List<string>();
        }
        public string Email { get; set; }
        
        [Display(Name = "Name")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Display(Name = "Surname(s)")]
        [StringLength(30)]
        public string LastName { get; set; }

        public ICollection<string> Roles { get; set; }

        public int EnrolledCourses { get; set; }
    }
}
