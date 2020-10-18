namespace Scio.Data.Models
{
    using System;

    using Scio.Data.Common.Models;
    using Scio.Data.Models.Enums;

    public class ForumVote : BaseDeletableModel<string>
    {
        public ForumVote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual VoteType Type { get; set; }

        public string PostId { get; set; }

        public virtual ForumPost Post { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
