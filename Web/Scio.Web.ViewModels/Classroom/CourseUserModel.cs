namespace Scio.Web.ViewModels.Classroom
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class CourseUserModel : IMapFrom<CourseUser>
    {
        public string Role { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Date { get; set; }
    }
}
