namespace Scio.Web.ViewModels.Classroom.Exams
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class SubmissionQuestionViewModel : IMapFrom<SubmissionQuestion>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public SubmissionAnswerViewModel[] Answers { get; set; }
    }
}
