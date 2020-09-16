namespace Scio.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class AnswerService : IAnswerService
    {
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public AnswerService(IDeletableEntityRepository<Answer> answersRepository)
        {
            this.answersRepository = answersRepository;
        }

        public async Task CreateAsync(string content, string questionId, string authorId)
        {
            var answer = new Answer
            {
                Content = content,
                QuestionId = questionId,
                AuthorId = authorId,
            };

            await this.answersRepository.AddAsync(answer);
            await this.answersRepository.SaveChangesAsync();
        }

        public TViewModel GetById<TViewModel>(string id)
            => this.answersRepository
            .AllAsNoTracking()
            .Where(p => p.Id == id)
            .To<TViewModel>()
            .FirstOrDefault();

        public async Task EditAsync(string id, string content)
        {
            var answer = this.answersRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            answer.Content = content;
            await this.answersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var answer = this.answersRepository
                .AllAsNoTracking()
                .FirstOrDefault(q => q.Id == id);

            this.answersRepository.Delete(answer);
            await this.answersRepository.SaveChangesAsync();
        }
    }
}
