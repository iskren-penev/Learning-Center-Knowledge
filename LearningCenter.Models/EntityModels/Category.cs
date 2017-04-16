namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;

    public class Category
    {
        private ICollection<Topic> topics;

        public Category()
        {
            this.Topics = new List<Topic>();
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
