namespace LearningCenter.Models.EntityModels
{
    using System.Collections.Generic;

    public class Quiz : Unit
    {
        private ICollection<Question> questionSet;

        public Quiz()
        {
            this.questionSet = new List<Question>();
        }

        public virtual ICollection<Question> QuestionSet
        {
            get { return this.questionSet; }
            set { this.questionSet = value; }
        }
    }
}
