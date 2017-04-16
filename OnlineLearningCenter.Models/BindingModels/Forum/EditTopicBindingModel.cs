namespace LearningCenter.Models.BindingModels.Forum
{
    using System.ComponentModel.DataAnnotations;

    public class EditTopicBindingModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(50)]
        public string Content { get; set; }
    }
}
