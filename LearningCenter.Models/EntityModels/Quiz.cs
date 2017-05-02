namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Quiz
    {
        private ICollection<Question> questionSet;

        public Quiz()
        {
            this.questionSet = new List<Question>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }


        [ForeignKey("Course")]
        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questionSet; }
            set { this.questionSet = value; }
        }
    }
}
