namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    public interface IForumVoteService
    {
        Task<int> CreateVoteAsync(int voteType, string postId, string userId);

        Task<int> UpdateVoteAsync(int voteType, string voteId);
    }
}
