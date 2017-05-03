namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Grade> grades;
        private ICollection<Course> enrolledCourses;
        private ICollection<Topic> topics;
        private ICollection<Reply> replies;

        public User()
        {
            this.grades = new List<Grade>();
            this.enrolledCourses = new List<Course>();
            this.topics = new List<Topic>();
            this.replies = new List<Reply>();
        }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        public virtual ICollection<Course> EnrolledCourses
        {
            get { return this.enrolledCourses; }
            set { this.enrolledCourses = value; }
        }
        
        public virtual ICollection<Topic> Topics
        {
            get { return this.topics; }
            set { this.topics = value; }
        }

        public virtual ICollection<Reply> Replies
        {
            get { return this.replies; }
            set { this.replies = value; }
        }

        public virtual ICollection<Grade> Grades
        {
            get { return this.grades; }
            set { this.grades = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
