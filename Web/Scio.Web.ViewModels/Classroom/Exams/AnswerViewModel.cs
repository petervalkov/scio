namespace Scio.Web.ViewModels.Classroom.Exams
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class AnswerViewModel : IMapFrom<ExamAnswer>
    {
        public string Id { get; set; }

        public string Content { get; set; }
    }
}
