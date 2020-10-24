namespace Scio.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;
    using Scio.Web.ViewModels.Forum.Comments;

    public class DetailsViewModel : IMapFrom<ForumPost>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int VotesCount { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorEmail { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }

        public string AnswerBody { get; set; }
    }
}
