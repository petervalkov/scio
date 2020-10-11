namespace Scio.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;
    using Scio.Web.ViewModels.Forum.Comments;

    public class QuestionDetailsViewModel : IMapFrom<ForumPost>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorEmail { get; set; }

        public IEnumerable<QuestionAnswerViewModel> Answers { get; set; }

        public IEnumerable<PostCommentViewModel> Comments { get; set; }
    }
}
