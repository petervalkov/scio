namespace Scio.Web.ViewModels.Classroom.Exams
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ResultAnswerViewModel : IMapFrom<SubmissionAnswer>
    {
        public string Content { get; set; }

        public bool? IsCorrect { get; set; }

        public bool ExamAnswerIsCorrect { get; set; }
    }
}
