namespace Scio.Web.ViewModels.Forum.Answers
{
    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditViewModel : IMapFrom<Answer>, IOwnable<string>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorEmail { get; set; }

        public string QuestionTitle { get; set; }

        public string QuestionContent { get; set; }

        public string QuestionId { get; set; }
    }
}
