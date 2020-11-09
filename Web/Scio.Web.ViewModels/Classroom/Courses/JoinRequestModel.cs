namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System.ComponentModel.DataAnnotations;

    public class JoinRequestModel
    {
        [Required]
        public string CourseId { get; set; }
    }
}
