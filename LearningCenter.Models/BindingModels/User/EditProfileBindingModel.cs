namespace LearningCenter.Models.BindingModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class EditProfileBindingModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Surname(s)")]
        public string LastName { get; set; }

       
    }
}
