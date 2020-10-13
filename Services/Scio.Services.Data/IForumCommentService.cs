namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IForumCommentService
    {
        Task<TViewModel> Create<TViewModel>(string body, string parentId, string postId, string authorId);

        IEnumerable<TViewModel> GetAllByParentId<TViewModel>(string postId, string parentId);
    }
}
