namespace Scio.Web.ViewModels.Classroom.Courses
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ExamViewModel : IMapFrom<Exam>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
