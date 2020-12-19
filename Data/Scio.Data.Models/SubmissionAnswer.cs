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

        public string Content { get; set; }

        public bool? IsCorrect { get; set; }

        public string ExamAnswerId { get; set; }

        public virtual ExamAnswer ExamAnswer { get; set; }

        public string ExamQuestionId { get; set; }

        public ExamQuestion ExamQuestion { get; set; }

        public string SubmissionQuestionId { get; set; }

        public virtual SubmissionQuestion SubmissionQuestion { get; set; }
    }
}
