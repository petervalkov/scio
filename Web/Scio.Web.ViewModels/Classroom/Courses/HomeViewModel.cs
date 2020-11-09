namespace Scio.Web.ViewModels.Classroom.Courses
{
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class HomeViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public CourseType Type { get; set; }

        public string AuthorDisplayName { get; set; }
    }
}
