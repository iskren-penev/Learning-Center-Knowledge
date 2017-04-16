namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        public string Description { get; set; }

        public string ContentUrl { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.asnwers; }
            set { this.asnwers = value; }
        }
    }
}
