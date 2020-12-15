namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;

    public class ExamAnswer : BaseDeletableModel<string>
    {
        public ExamAnswer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SubmissionAnswers = new HashSet<SubmissionAnswer>();
        }

        public string Content { get; set; }

        public int Points { get; set; }

        public bool IsCorrect { get; set; }

        public string QuestionId { get; set; }

        public virtual ExamQuestion Question { get; set; }

        public virtual ICollection<SubmissionAnswer> SubmissionAnswers { get; set; }
    }
}
