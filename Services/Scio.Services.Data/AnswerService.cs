namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;

    public class AnswerService : IAnswerService
    {
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public AnswerService(IDeletableEntityRepository<Answer> answersRepository)
        {
            this.answersRepository = answersRepository;
        }

        public async Task<string> CreateAsync(string content, string questionId, string authorId)
        {
            var answer = new Answer
            {
                Content = content,
                QuestionId = questionId,
                AuthorId = authorId,
            };

            await this.answersRepository.AddAsync(answer);
            await this.answersRepository.SaveChangesAsync();

            return answer.Id;
        }
    }
}
