namespace LearningCenter.Models.ViewModels.Units
{
    using System.ComponentModel.DataAnnotations;

    public class AddUnitViewModel
    {
        public string Title { get; set; }

        [Display(Name = "Content URL")]
        public string ContentUrl { get; set; }
    }
}
