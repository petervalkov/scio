namespace Scio.Web.ViewModels.Forum.Answers
{
    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class EditInputModel : IMapFrom<Answer>, IOwnable<string>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string QuestionId { get; set; }

        public string AuthorId { get; set; }
    }
}
