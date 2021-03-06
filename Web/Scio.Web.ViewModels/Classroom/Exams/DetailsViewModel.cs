﻿namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class DetailsViewModel : IMapFrom<Exam>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime Opens { get; set; }

        public DateTime Closes { get; set; }

        public int Duration { get; set; }

        public bool AcceptAfterClosing { get; set; }

        public bool AcceptExpiredTime { get; set; }

        public bool RandomVariant { get; set; }

        public int QuestionsPerVariant { get; set; }

        public int AnswersPerQuestion { get; set; }

        public string CourseId { get; set; }

        public string AuthorId { get; set; }

        public QuestionViewModel[] Questions { get; set; }

        public ICollection<ExamSubmissionViewModel> Submissions { get; set; }
    }
}
