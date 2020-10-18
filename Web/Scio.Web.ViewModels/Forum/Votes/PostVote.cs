namespace Scio.Web.ViewModels.Forum.Votes
{
    using AutoMapper;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class PostVote : IMapFrom<ForumVote>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public int Value { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumVote, PostVote>()
                  .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                  .ForMember(dst => dst.Value, opt => opt.MapFrom(src => (int)src.Type));
        }
    }
}
