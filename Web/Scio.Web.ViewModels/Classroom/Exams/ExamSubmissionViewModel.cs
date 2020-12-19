namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ExamSubmissionViewModel : IMapFrom<Submission>
    {
        public string Id { get; set; }

        public string AuthorDisplayName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Score { get; set; }
    }
}
