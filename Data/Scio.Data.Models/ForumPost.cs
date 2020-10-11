namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;

    public class ForumPost : BaseDeletableModel<string>
    {
        public ForumPost()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Answers = new HashSet<ForumPost>();
            this.Comments = new HashSet<ForumComment>();
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string QuestionId { get; set; }

        public virtual ForumPost Question { get; set; }

        public virtual ICollection<ForumPost> Answers { get; set; }

        public virtual ICollection<ForumComment> Comments { get; set; }
    }
}
