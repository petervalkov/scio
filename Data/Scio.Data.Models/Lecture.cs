namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;

    public class Lecture : BaseDeletableModel<string>
    {
        public Lecture()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Resources = new HashSet<Resource>();
        }

        public string Title { get; set; }

        public DateTime? ScheduledFor { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
