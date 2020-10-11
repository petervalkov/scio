namespace Scio.Web.ViewModels.Forum.Common
{
    using Scio.Data.Common.Models;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ResourceOwnerValidationModel : IMapFrom<ForumPost>, IOwnable<string>
    {
        public string AuthorId { get; set; }

        public string QuestionId { get; set; }
    }
}
