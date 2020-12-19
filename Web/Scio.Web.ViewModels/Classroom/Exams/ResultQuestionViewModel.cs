namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ResultQuestionViewModel : IMapFrom<SubmissionQuestion>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public virtual ICollection<ResultAnswerViewModel> Answers { get; set; }
    }
}
