namespace Scio.Web.ViewModels.Forum.Posts
{
    using AutoMapper;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class VoteModel : IMapFrom<ForumVote>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public int Value { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumVote, VoteModel>()
                  .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                  .ForMember(dst => dst.Value, opt => opt.MapFrom(src => (int)src.Type));
        }
    }
}
