namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;

    public class Submission : BaseDeletableModel<string>
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Answers = new HashSet<SubmissionAnswer>();
        }

        public int Score { get; set; }

        public DateTime Started { get; set; }

        public DateTime Ended { get; set; }

        public string ExamId { get; set; }

        public virtual Exam Exam { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<SubmissionAnswer> Answers { get; set; }
    }
}
