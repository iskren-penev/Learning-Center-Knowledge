namespace LearningCenter.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Unit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        public string ContentUrl { get; set; }

        [ForeignKey("Course")]
        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
