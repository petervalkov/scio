namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    public interface IForumCommentService
    {
        Task<TResponseModel> Create<TResponseModel>(string body, string parentId, string postId, string authorId);
    }
}
