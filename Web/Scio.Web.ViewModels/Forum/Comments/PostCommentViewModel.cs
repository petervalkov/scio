namespace Scio.Web.ViewModels.Forum.Comments
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class PostCommentViewModel : IMapFrom<ForumComment>
    {
        public string Body { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorEmail { get; set; }
    }
}
