namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ResourceViewModel : IMapFrom<Resource>
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
