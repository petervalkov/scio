namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class DetailsViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public CourseType Type { get; set; }

        public string AuthorId { get; set; }

        public string AuthorDisplayName { get; set; }

        public string AuthorImageUrl { get; set; }

        public string ShortTitle => this.Title.Length > 30 ? this.Title.Substring(0, 30) + "..." : this.Title;

        public virtual IEnumerable<LectureViewModel> Lectures { get; set; }

        public virtual IEnumerable<UserViewModel> Users { get; set; }

        public virtual IEnumerable<DetailsExamViewModel> Exams { get; set; }
    }
}
