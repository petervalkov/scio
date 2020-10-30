namespace Scio.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Scio.Data.Models;
    using Scio.Services.Mapping;
    using Scio.Web.ViewModels.Forum.Comments;

    public class DetailsViewModel : IMapFrom<ForumPost>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int TotalVotes { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string AuthorDisplayName { get; set; }

        public string AuthorImageUrl { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }

        public string AnswerBody { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumPost, DetailsViewModel>()
                .ForMember(dst => dst.TotalVotes, opt => opt.MapFrom(src => src.Votes.Sum(x => (int)x.Type)));
        }
    }
}
