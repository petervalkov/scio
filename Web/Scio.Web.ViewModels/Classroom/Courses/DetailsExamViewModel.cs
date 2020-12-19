namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class DetailsExamViewModel : IMapFrom<Exam>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime? Opens { get; set; }

        public DateTime? Closes { get; set; }

        public int SubmissionsCount { get; set; }
    }
}
