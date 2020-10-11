namespace Scio.Web.ViewModels.Forum.Posts
{
    using System;

    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditAnswerViewModel : IMapFrom<ForumPost>, IOwnable<string>
    {
        public string Id { get; set; }

        public string Body { get; set; }

        public string AuthorId { get; set; }

        public string AuthorEmail { get; set; }

        public string QuestionTitle { get; set; }

        public string QuestionBody { get; set; }

        public string QuestionId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
