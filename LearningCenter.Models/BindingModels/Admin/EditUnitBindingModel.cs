namespace LearningCenter.Models.BindingModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class EditUnitBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        public string ContentUrl { get; set; }
    }
}
