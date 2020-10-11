namespace Scio.Web.ViewModels.Forum.Posts
{
    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditAnswerInputModel : IMapFrom<ForumPost>, IOwnable<string>
    {
        public string Id { get; set; }

        public string Body { get; set; }

        public string QuestionId { get; set; }

        public string AuthorId { get; set; }
    }
}
