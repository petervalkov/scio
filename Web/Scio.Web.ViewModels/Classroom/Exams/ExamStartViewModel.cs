namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ExamStartViewModel : IMapFrom<Exam>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime Opens { get; set; }

        public DateTime Closes { get; set; }

        public int Duration { get; set; }

        public string CourseTitle { get; set; }

        public string CourseId { get; set; }
    }
}
