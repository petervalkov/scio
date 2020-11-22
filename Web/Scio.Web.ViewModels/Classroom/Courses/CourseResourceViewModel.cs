namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class CourseResourceViewModel : IMapFrom<Resource>
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
