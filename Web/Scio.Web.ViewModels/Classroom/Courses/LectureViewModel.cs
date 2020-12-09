namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class LectureViewModel : IMapFrom<Lecture>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<ResourceViewModel> Resources { get; set; }
    }
}
