namespace Scio.Web.ViewModels.Classroom.Exams
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class AnswerInputModel : IMapTo<ExamAnswer>
    {
        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public int Points { get; set; }
    }
}
