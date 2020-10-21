namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ForumPostService : IForumPostService
    {
        private readonly IDeletableEntityRepository<ForumPost> forumPostsRepository;

        public ForumPostService(IDeletableEntityRepository<ForumPost> forumPostsRepository)
        {
            this.forumPostsRepository = forumPostsRepository;
        }

        public IEnumerable<TViewModel> GetAllQuestions<TViewModel>()
            => this.forumPostsRepository
            .AllAsNoTracking()
            .Where(p => p.QuestionId == null)
            .To<TViewModel>()
            .ToList();

        public TViewModel GetById<TViewModel>(string id)
            => this.forumPostsRepository
            .AllAsNoTracking()
            .Where(p => p.Id == id)
            .To<TViewModel>()
            .FirstOrDefault();

        public async Task<string> CreateAsync(string title, string body, string questionId, string authorId)
        {
            var question = new ForumPost
            {
                Title = title,
                Body = body,
                AuthorId = authorId,
                QuestionId = questionId,
            };

            await this.forumPostsRepository.AddAsync(question);
            await this.forumPostsRepository.SaveChangesAsync();

            return question.Id;
        }

        public async Task EditAsync(string id, string title, string body)
        {
            var question = this.forumPostsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            question.Title = title;
            question.Body = body;

            await this.forumPostsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var question = this.forumPostsRepository
                .AllAsNoTracking()
                .FirstOrDefault(q => q.Id == id);

            this.forumPostsRepository.Delete(question);
            await this.forumPostsRepository.SaveChangesAsync();
        }

        public TValidationModel SearchForVote<TValidationModel>(string postId, string userId)
        {
            var post = this.forumPostsRepository
              .AllWithDeletedIncluding("Votes")
              .Where(p => p.Id == postId)
              .Select(p => new ForumPost
              {
                  Votes = p.Votes.Where(v => v.UserId == userId).ToList(),
              })
              .FirstOrDefault();

            return AutoMapperConfig.MapperInstance.Map<TValidationModel>(post);
        }
    }
}
