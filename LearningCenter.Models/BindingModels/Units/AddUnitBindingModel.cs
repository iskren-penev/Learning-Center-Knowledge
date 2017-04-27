namespace LearningCenter.Models.BindingModels.Units
{
    using System.ComponentModel.DataAnnotations;

    public class AddUnitBindingModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        public string ContentUrl { get; set; }
    }
}
