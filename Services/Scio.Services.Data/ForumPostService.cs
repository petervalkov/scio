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

        public IEnumerable<TViewModel> Get<TViewModel>()
            => this.forumPostsRepository
            .AllAsNoTracking()
            .Where(p => p.QuestionId == null)
            .To<TViewModel>()
            .ToList();

        public TModel Get<TModel>(string id, string authorId = null)
            => this.forumPostsRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id && (authorId == null || x.AuthorId == authorId))
            .To<TModel>()
            .FirstOrDefault();

        public TModel Find<TModel>(string id, string authorId = null, string questionId = null)
            => this.forumPostsRepository
            .All()
            .Where(x => x.Id == id
                && (authorId == null || x.AuthorId == authorId)
                && (questionId == null || x.QuestionId == questionId))
            .ToList()
            .AsQueryable()
            .To<TModel>()
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
            var question = await this.forumPostsRepository.FindByIdAsync(id);

            question.Title = title;
            question.Body = body;

            await this.forumPostsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var post = await this.forumPostsRepository.FindByIdAsync(id);

            this.forumPostsRepository.Delete(post);
            await this.forumPostsRepository.SaveChangesAsync();
        }
    }
}
