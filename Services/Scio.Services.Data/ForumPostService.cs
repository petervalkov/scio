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

        public async Task<string> CreateQuestionAsync(string title, string body, string authorId)
        {
            var question = new ForumPost
            {
                Title = title,
                Body = body,
                AuthorId = authorId,
            };

            await this.forumPostsRepository.AddAsync(question);
            await this.forumPostsRepository.SaveChangesAsync();

            return question.Id;
        }

        public async Task EditQuestionAsync(string id, string title, string body)
        {
            var question = this.forumPostsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            question.Title = title;
            question.Body = body;

            await this.forumPostsRepository.SaveChangesAsync();
        }

        public async Task CreateAnswerAsync(string body, string questionId, string authorId)
        {
            var answer = new ForumPost
            {
                Body = body,
                QuestionId = questionId,
                AuthorId = authorId,
            };

            await this.forumPostsRepository.AddAsync(answer);
            await this.forumPostsRepository.SaveChangesAsync();
        }

        public async Task EditAnswerAsync(string id, string body)
        {
            var answer = this.forumPostsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            answer.Body = body;
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
    }
}
