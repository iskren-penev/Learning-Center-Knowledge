namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Question
    {
        private ICollection<Answer> asnwers;

        public Question()
        {
            this.asnwers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Description { get; set; }
        
        [ForeignKey("Quiz")]
        public int? QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.asnwers; }
            set { this.asnwers = value; }
        }
    }
}
