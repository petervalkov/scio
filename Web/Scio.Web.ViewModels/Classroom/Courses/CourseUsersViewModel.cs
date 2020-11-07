namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;

    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class CourseUsersViewModel : IMapFrom<CourseUser>
    {
        public string UserEmail { get; set; }

        public DateTime CreatedOn { get; set; }

        public CourseUserStatus Status { get; set; }
    }
}
