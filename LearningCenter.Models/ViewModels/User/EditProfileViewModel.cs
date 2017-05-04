namespace LearningCenter.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class EditProfileViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }
        
        [StringLength(30, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string LastName { get; set; }
    }
}
