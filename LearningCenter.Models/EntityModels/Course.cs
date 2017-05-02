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
        private ICollection<Quiz> quizzes;
        
        public Course()
        {
            this.students = new List<User>();
            this.units = new List<Unit>();
            this.grades= new List<Grade>();
            this.quizzes= new List<Quiz>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string ShortDescription { get; set; }

        [Required]
        [MinLength(250)]
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

        public virtual ICollection<Quiz> Quizzes
        {
            get { return this.quizzes; }
            set { this.quizzes = value; }
        }

        public virtual ICollection<Grade> Grades
        {
            get { return this.grades; }
            set { this.grades = value; }
        }
    }
}
