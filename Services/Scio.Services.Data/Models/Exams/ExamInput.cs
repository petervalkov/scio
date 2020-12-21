namespace Scio.Services.Data.Models.Exams
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ExamInput : IMapTo<Exam>
    {
        public string Title { get; set; }

        public int? Duration { get; set; }

        public DateTime? Opens { get; set; }

        public DateTime? Closes { get; set; }

        public bool AcceptAfterClosing { get; set; }

        public bool AcceptExpiredTime { get; set; }

        public bool RandomVariant { get; set; }

        public int QuestionsPerVariant { get; set; }

        public int AnswersPerQuestion { get; set; }

        public string AuthorId { get; set; }

        public string CourseId { get; set; }

        public string CourseTitle { get; set; }

        public ICollection<QuestionInput> Questions { get; set; }
    }
}
