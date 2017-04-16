namespace LearningCenter.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class ProfileViewModel
    {
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

    }
}
