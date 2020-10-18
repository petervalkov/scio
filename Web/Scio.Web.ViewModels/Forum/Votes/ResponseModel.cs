namespace Scio.Web.ViewModels.Forum.Votes
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ResponseModel : IMapFrom<ForumPost>
    {
        public int VotesSum { get; set; }

        public string Message { get; set; }
    }
}
