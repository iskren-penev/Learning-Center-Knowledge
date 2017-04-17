namespace LearningCenter.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class EditProfileViewModel
    {
        [Display(Name = "Name")]
        public string FirstName { get; set; }
        [Display(Name = "Surname(s)")]
        public string LastName { get; set; }
    }
}
