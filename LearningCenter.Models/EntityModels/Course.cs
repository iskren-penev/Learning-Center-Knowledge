namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        private ICollection<User> students;
        private ICollection<Unit> units;
        private ICollection<Grade> grades;


        public Course()
        {
            this.students = new List<User>();
            this.units = new List<Unit>();
            this.grades= new List<Grade>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        public virtual ICollection<User> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }
        public virtual ICollection<Unit> Units
        {
            get { return this.units; }
            set { this.units = value; }
        }

        [ForeignKey("Quiz")]
        public int? QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<Grade> Grades
        {
            get { return this.grades; }
            set { this.grades = value; }
        }
    }
}
