namespace Scio.Web.ViewModels.Forum.Questions
{
    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditViewModel : IMapFrom<Question>, IOwnable<string>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }
    }
}
