namespace Scio.Web.ViewModels.Forum.Votes
{
    using System.Linq;

    using AutoMapper;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class PostValidationModel : IHaveCustomMappings
    {
        public PostVote Vote { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumPost, PostValidationModel>()
                .ForMember(dst => dst.Vote, opt => opt.MapFrom(src => src.Votes.FirstOrDefault()));
        }
    }
}
