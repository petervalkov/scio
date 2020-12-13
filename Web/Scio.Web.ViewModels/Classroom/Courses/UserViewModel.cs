namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;

    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class UserViewModel : IMapFrom<CourseUser>
    {
        public string UserId { get; set; }

        public string UserDisplayName { get; set; }

        public string UserEmail { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Status { get; set; }

        public string Role { get; set; }
    }
}
