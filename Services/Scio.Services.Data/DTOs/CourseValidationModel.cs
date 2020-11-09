namespace Scio.Services.Data.DTOs
{
    using Scio.Data.Models.Enums;

    public class CourseValidationModel
    {
        public string Id { get; set; }

        public CourseType Type { get; set; }

        public string AuthorId { get; set; }

        public CourseUserStatus? UserStatus { get; set; }
    }
}
