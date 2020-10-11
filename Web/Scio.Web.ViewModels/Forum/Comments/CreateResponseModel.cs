namespace Scio.Web.ViewModels.Forum.Comments
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class CreateResponseModel : IMapFrom<ForumComment>
    {
        public string Body { get; set; }

        public string AuthorId { get; set; }

        public string PostId { get; set; }

        public string ParentId { get; set; }

        public string CreatedOn { get; set; }
    }
}
