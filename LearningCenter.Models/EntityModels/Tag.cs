namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;

    public class Tag
    {
        private ICollection<Topic> topics;

        public Tag()
        {
            this.topics = new List<Topic>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Topic> Topics
        {
            get { return this.topics; }
            set { this.topics = value; }
        }
    }
}
