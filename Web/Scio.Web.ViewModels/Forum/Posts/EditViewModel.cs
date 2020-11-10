namespace Scio.Web.ViewModels.Forum.Posts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using Scio.Common;
    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditViewModel : IMapFrom<ForumPost>, IOwnable<string>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = FieldName.Title)]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = Validation.MinStringLength,
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Title { get; set; }

        public string Body { get; set; }

        public string AnswerBody { get; set; }

        public string QuestionId { get; set; }

        public int TotalVotes { get; set; }

        public int CommentsCount { get; set; }

        public string AuthorId { get; set; }

        public string QuestionTitle { get; set; }

        public string QuestionBody { get; set; }

        public DateTime QuestionCreatedOn { get; set; }

        public int QuestionCommentsCount { get; set; }

        public int QuestionAnswersCount { get; set; }

        public int QuestionTotalVotes { get; set; }

        public string QuestionAuthorId { get; set; }

        public string QuestionAuthorEmail { get; set; }

        public string QuestionAuthorImageUrl { get; set; }

        public string ShortTitle => this.Title.Length > 30 ? this.Title.Substring(0, 30) + "..." : this.Title;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumPost, EditViewModel>()
                .ForMember(dst => dst.AnswerBody, opt => opt.MapFrom(src => src.Body))
                .ForMember(dst => dst.TotalVotes, opt => opt.MapFrom(src => src.Votes.Sum(x => (int)x.Type)))
                .ForMember(dst => dst.QuestionTotalVotes, opt => opt.MapFrom(src => src.Question.Votes.Sum(x => (int)x.Type)));
        }
    }
}
