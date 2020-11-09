namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Data.Models.Enums;

    public class UpdateUserStatusModel
    {
        [Required]
        public string CourseId { get; set; }

        [Required]
        public string UserId { get; set; }

        [EnumDataType(typeof(CourseUserStatus))]
        public int Status { get; set; }
    }
}
