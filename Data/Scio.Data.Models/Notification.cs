namespace Scio.Data.Models
{
    using System;

    using Scio.Data.Common.Models;
    using Scio.Data.Models.Enums;

    public class Notification : BaseDeletableModel<string>
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public string Link { get; set; }

        public NotificationType Type { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string RecipientId { get; set; }

        public virtual ApplicationUser Recipient { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
