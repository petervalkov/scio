namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class SettingsViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(
           Validation.MaxTitleLength,
           MinimumLength = Validation.MinStringLength,
           ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Title { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = ErrorMessage.MinLength)]
        public string Description { get; set; }

        [Range(0, 1, ErrorMessage = ErrorMessage.InvalidCourseType)]
        public CourseType Type { get; set; }

        public string AuthorId { get; set; }

        public string AuthorDisplayName { get; set; }

        public string AuthorImageUrl { get; set; }

        public string ShortTitle => this.Title.Length > 30 ? this.Title.Substring(0, 30) + "..." : this.Title;

        public IEnumerable<UserViewModel> Users { get; set; }

        public IEnumerable<LectureViewModel> Lectures { get; set; }

        public IEnumerable<SettingsExamViewModel> Exams { get; set; }
    }
}
