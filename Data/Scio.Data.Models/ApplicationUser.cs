﻿// ReSharper disable VirtualMemberCallInConstructor
namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using Scio.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.ForumPosts = new HashSet<ForumPost>();
            this.ForumComments = new HashSet<ForumComment>();
            this.ForumVotes = new HashSet<ForumVote>();
            this.ResourcesUploaded = new HashSet<Resource>();
            this.Activity = new HashSet<Notification>();
            this.Notifications = new HashSet<Notification>();
        }

        public string DisplayName { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<ForumPost> ForumPosts { get; set; }

        public virtual ICollection<ForumComment> ForumComments { get; set; }

        public virtual ICollection<ForumVote> ForumVotes { get; set; }

        public virtual ICollection<Course> CoursesCreated { get; set; }

        public virtual ICollection<CourseUser> CoursesEnrolled { get; set; }

        public virtual ICollection<Resource> ResourcesUploaded { get; set; }

        public virtual ICollection<Notification> Activity { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
