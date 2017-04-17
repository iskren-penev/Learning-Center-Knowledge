namespace LearningCenter.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class ProfileViewModel
    {
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname(s)")]
        public string LastName { get; set; }

    }
}
