namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public string Text { get; set; }

        public string Link { get; set; }

        public string AuthorDisplayName { get; set; }

        public string AuthorId { get; set; }

        public string AuthorImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
