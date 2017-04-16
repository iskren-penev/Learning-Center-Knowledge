namespace LearningCenter.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0,100)]
        public int Result { get; set; }

        [ForeignKey("Course")]
        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }

        

        public virtual User Student { get; set; }
    }
}
