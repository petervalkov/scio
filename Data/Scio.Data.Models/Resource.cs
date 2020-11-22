namespace Scio.Data.Models
{
    using System;

    using Scio.Data.Common.Models;

    public class Resource : BaseDeletableModel<string>
    {
        public Resource()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Url { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
