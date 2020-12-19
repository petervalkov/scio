namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ResultViewModel : IMapFrom<Submission>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime Ended { get; set; }

        public string ExamTitle { get; set; }

        public ICollection<ResultQuestionViewModel> Questions { get; set; }

        public string AuthorId { get; set; }

        public string AuthorDisplayName { get; set; }
    }
}
