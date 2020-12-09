namespace Scio.Web.ViewModels.Classroom.Lectures
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class InputModel
    {
        public string Title { get; set; }

        public string CourseId { get; set; }

        public IFormFile[] Files { get; set; }
    }
}
