namespace Scio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Common.Models;
    using Scio.Data.Models.Enums;

    public class Course : BaseDeletableModel<string>
    {
        public Course()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<CourseUser>();
            this.Resources = new HashSet<Resource>();
            this.Lectures = new HashSet<Lecture>();
            this.Exams = new HashSet<Exam>();
            this.Notifications = new HashSet<Notification>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public CourseType Type { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<CourseUser> Users { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
