namespace Scio.Web.ViewModels.Forum.Posts
{
    using System;

    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditViewModel : IMapFrom<ForumPost>, IOwnable<string>
    {
        // Common properties
        public string Id { get; set; }

        public string Body { get; set; }

        public string AuthorId { get; set; }

        public string AuthorEmail { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        // Question properties
        public string Title { get; set; }

        // Answer properties
        public string QuestionId { get; set; }

        public string QuestionTitle { get; set; }

        public string QuestionBody { get; set; }

        public DateTime QuestionCreatedOn { get; set; }

        public string QuestionAuthorEmail { get; set; }
    }
}
