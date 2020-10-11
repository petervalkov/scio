namespace Scio.Web.ViewModels.Forum.Posts
{
    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditQuestionViewModel : IMapFrom<ForumPost>, IOwnable<string>
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string AuthorId { get; set; }
    }
}
