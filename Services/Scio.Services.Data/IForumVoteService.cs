namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    public interface IForumVoteService
    {
        Task<int> CreateAsync(int voteType, string postId, string userId);

        Task<int> UpdateAsync(int voteType, string voteId);

        TModel Get<TModel>(string postId, string userId);
    }
}
