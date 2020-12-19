namespace Scio.Web.ViewModels.Classroom.Exams
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class SubmissionAnswerViewModel : IMapFrom<SubmissionAnswer>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public bool IsCorrect { get; set; }
    }
}
