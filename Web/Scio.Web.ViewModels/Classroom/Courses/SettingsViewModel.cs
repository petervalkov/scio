namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class SettingsViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortTitle => this.Title.Length > 30 ? this.Title.Substring(0, 30) + "..." : this.Title;

        public IEnumerable<CourseUsersViewModel> Users { get; set; }
    }
}
