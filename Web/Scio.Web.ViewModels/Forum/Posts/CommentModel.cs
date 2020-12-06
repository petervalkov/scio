namespace Scio.Web.ViewModels.Forum.Posts
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class CommentModel : IMapFrom<ForumComment>
    {
        public string Body { get; set; }

        public string CreatedOn { get; set; }

        public string AuthorEmail { get; set; }

        public string AuthorId { get; set; }

        public string PostId { get; set; }
    }
}
