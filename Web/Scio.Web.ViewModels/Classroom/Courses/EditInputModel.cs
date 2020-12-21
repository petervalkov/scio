namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;

    public class EditInputModel
    {
        [Required]
        public string CourseId { get; set; }

        [Required]
        [StringLength(
           Validation.MaxTitleLength,
           MinimumLength = Validation.MinStringLength,
           ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = 50, // CHECK
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Description { get; set; }

        [Range(0, 1, ErrorMessage = ErrorMessage.InvalidCourseType)]
        public int Type { get; set; }
    }
}
