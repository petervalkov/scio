namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;

    public class ForumComment : BaseDeletableModel<string>
    {
        public ForumComment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<ForumComment>();
        }

        public string Body { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string PostId { get; set; }

        public virtual ForumPost Post { get; set; }

        public string ParentId { get; set; }

        public virtual ForumComment Parent { get; set; }

        public virtual ICollection<ForumComment> Comments { get; set; }
    }
}
