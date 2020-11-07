namespace Scio.Web.ViewModels.Classroom.Courses
{
    using AutoMapper;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class CourseUserValidationModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public CourseType Type { get; set; }

        public bool ContainsUser { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, CourseUserValidationModel>()
                .ForMember(dst => dst.ContainsUser, opt => opt.MapFrom(src => src.Users.Count > 0));
        }
    }
}
