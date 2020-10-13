namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ForumCommentService : IForumCommentService
    {
        private readonly IDeletableEntityRepository<ForumComment> commentsRepository;

        public ForumCommentService(IDeletableEntityRepository<ForumComment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task<TViewModel> Create<TViewModel>(string body, string parentId, string postId, string authorId)
        {
            var comment = new ForumComment
            {
                Body = body,
                ParentId = parentId,
                PostId = postId,
                AuthorId = authorId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();

            return AutoMapperConfig.MapperInstance.Map<TViewModel>(comment);
        }

        public IEnumerable<TViewModel> GetAllByParentId<TViewModel>(string postId, string parentId)
            => this.commentsRepository
               .AllAsNoTracking()
               .Where(p => p.PostId == postId && p.ParentId == parentId)
               .To<TViewModel>()
               .ToList();
    }
}
