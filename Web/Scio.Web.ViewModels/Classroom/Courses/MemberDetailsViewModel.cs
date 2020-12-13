namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System.Collections.Generic;

    using AutoMapper;

    public class MemberDetailsViewModel : DetailsViewModel
    {
        [IgnoreMap]
        public override IEnumerable<LectureViewModel> Lectures { get; set; }

        [IgnoreMap]
        public override IEnumerable<UserViewModel> Users { get; set; }
    }
}
