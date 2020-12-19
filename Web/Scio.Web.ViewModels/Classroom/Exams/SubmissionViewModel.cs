namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class SubmissionViewModel : IMapFrom<Submission>
    {
        public string Id { get; set; }

        public string ExamTitle { get; set; }

        public string ExamCourseTitle { get; set; }

        public SubmissionQuestionViewModel[] Questions { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
