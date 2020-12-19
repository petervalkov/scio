namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;

    public class SubmissionQuestion : BaseDeletableModel<string>
    {
        public SubmissionQuestion()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        public virtual ICollection<SubmissionAnswer> Answers { get; set; }
    }
}
