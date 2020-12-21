namespace Scio.Services.Data.DTOs
{
    using Scio.Data.Models.Enums;

    public class CourseValidationModel
    {
        public string Id { get; set; }

        public int Type { get; set; }

        public string AuthorId { get; set; }

        public int? UserStatus { get; set; }

        public int? UserRole { get; set; }
    }
}
