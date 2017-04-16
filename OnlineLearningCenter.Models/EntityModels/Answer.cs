namespace LearningCenter.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
