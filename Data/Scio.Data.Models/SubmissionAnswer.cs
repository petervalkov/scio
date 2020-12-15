namespace Scio.Data.Models
{
    using System;

    using Scio.Data.Common.Models;

    public class SubmissionAnswer : BaseDeletableModel<string>
    {
        public SubmissionAnswer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ExamAnswerId { get; set; }

        public virtual ExamAnswer ExamAnswer { get; set; }

        public string ExamQuestionId { get; set; }

        public ExamQuestion ExamQuestion { get; set; }
    }
}
