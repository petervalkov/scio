namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class QuestionViewModel : IMapFrom<ExamQuestion>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public virtual AnswerViewModel[] Answers { get; set; }
    }
}
