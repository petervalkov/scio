namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;
    using Scio.Data.Models.Enums;

    public class ExamQuestion : BaseDeletableModel<string>
    {
        public ExamQuestion()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Answers = new HashSet<ExamAnswer>();
            this.SubmissionAnswers = new HashSet<SubmissionAnswer>();
        }

        public string Content { get; set; }

        public QuestionType Type { get; set; }

        public string ExamId { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual ICollection<ExamAnswer> Answers { get; set; }

        public virtual ICollection<SubmissionAnswer> SubmissionAnswers { get; set; }
    }
}
