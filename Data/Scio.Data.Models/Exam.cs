namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;

    public class Exam : BaseDeletableModel<string>
    {
        public Exam()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Questions = new HashSet<ExamQuestion>();
            this.Submissions = new HashSet<Submission>();
        }

        public string Title { get; set; }

        public DateTime? Opens { get; set; }

        public DateTime? Closes { get; set; }

        public int? Duration { get; set; }

        public bool AcceptAfterClosing { get; set; }

        public bool AcceptExpiredTime { get; set; }

        public bool RandomVariant { get; set; }

        public int QuestionsPerVariant { get; set; }

        public int AnswersPerQuestion { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<ExamQuestion> Questions { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
