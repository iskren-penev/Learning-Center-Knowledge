namespace LearningCenter.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class AddUnitViewModel
    {
        public string Title { get; set; }

        [Display(Name = "Content URL")]
        public string ContentUrl { get; set; }
    }
}
