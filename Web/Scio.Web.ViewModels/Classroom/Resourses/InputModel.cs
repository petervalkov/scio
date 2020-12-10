namespace Scio.Web.ViewModels.Classroom.Resourses
{
    using Microsoft.AspNetCore.Http;

    public class InputModel
    {
        public string LectureId { get; set; }

        public string CourseId { get; set; }

        public IFormFile[] Files { get; set; }
    }
}
