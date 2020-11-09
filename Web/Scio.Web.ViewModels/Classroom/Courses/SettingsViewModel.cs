namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class SettingsViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public IEnumerable<UsersViewModel> Users { get; set; }
    }
}
