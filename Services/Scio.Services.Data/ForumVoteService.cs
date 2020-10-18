namespace Scio.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;

    public class ForumVoteService : IForumVoteService
    {
        private readonly IDeletableEntityRepository<ForumVote> forumVotesRepository;

        public ForumVoteService(IDeletableEntityRepository<ForumVote> forumVotesRepository)
        {
            this.forumVotesRepository = forumVotesRepository;
        }

        public async Task<int> CreateVoteAsync(int voteType, string postId, string userId)
        {
            var vote = new ForumVote
            {
                PostId = postId,
                UserId = userId,
                Type = (VoteType)voteType,
            };

            await this.forumVotesRepository.AddAsync(vote);
            await this.forumVotesRepository.SaveChangesAsync();

            return this.forumVotesRepository.AllWithDeleted().Where(v => v.PostId == vote.PostId).Select(v => (int)v.Type).Sum();
        }

        public async Task<int> UpdateVoteAsync(int voteType, string voteId)
        {
            var vote = this.forumVotesRepository.AllWithDeleted().FirstOrDefault(v => v.Id == voteId);

            if (vote.IsDeleted)
            {
                vote.IsDeleted = false;
            }

            vote.Type = (VoteType)voteType;
            await this.forumVotesRepository.SaveChangesAsync();

            return this.forumVotesRepository.AllWithDeleted().Where(v => v.PostId == vote.PostId).Select(v => (int)v.Type).Sum();
        }
    }
}
