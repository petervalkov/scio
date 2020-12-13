namespace Scio.Data.Models
{
    using System;

    using Scio.Data.Common.Models;
    using Scio.Data.Models.Enums;

    public class CourseUser : IAuditInfo, IDeletableEntity
    {
        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string UserId { get; set; }

        public CourseUserStatus Status { get; set; }

        public CourseRole Role { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
