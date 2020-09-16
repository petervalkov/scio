namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class QuestionService : IQuestionService
    {
        private readonly IDeletableEntityRepository<Question> questionRepository;

        public QuestionService(IDeletableEntityRepository<Question> questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<string> CreateAsync(string title, string content, string authorId)
        {
            var question = new Question
            {
                Title = title,
                Content = content,
                AuthorId = authorId,
            };

            await this.questionRepository.AddAsync(question);
            await this.questionRepository.SaveChangesAsync();

            return question.Id;
        }

        public IEnumerable<TViewModel> GetAll<TViewModel>()
            => this.questionRepository
            .AllAsNoTracking()
            .To<TViewModel>()
            .ToList();

        public TViewModel GetById<TViewModel>(string id)
            => this.questionRepository
            .AllAsNoTracking()
            .Where(p => p.Id == id)
            .To<TViewModel>()
            .FirstOrDefault();

        public async Task EditAsync(string id, string title, string content)
        {
            var question = this.questionRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            question.Title = title;
            question.Content = content;

            await this.questionRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var question = this.questionRepository
                .AllAsNoTracking()
                .FirstOrDefault(q => q.Id == id);

            this.questionRepository.Delete(question);
            await this.questionRepository.SaveChangesAsync();
        }
    }
}
