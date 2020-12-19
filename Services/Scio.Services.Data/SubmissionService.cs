namespace Scio.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Services.Data.DTOs;
    using Scio.Services.Mapping;

    public class SubmissionService : ISubmissionService
    {
        private readonly IDeletableEntityRepository<Submission> submissionsRepository;
        private readonly IDeletableEntityRepository<Exam> examsRepository;
        private readonly IDeletableEntityRepository<SubmissionAnswer> submissionAnswersRepository;

        public SubmissionService(
            IDeletableEntityRepository<Submission> submissionsRepository,
            IDeletableEntityRepository<Exam> examsRepository,
            IDeletableEntityRepository<SubmissionAnswer> submissionAnswersRepository)
        {
            this.submissionsRepository = submissionsRepository;
            this.examsRepository = examsRepository;
            this.submissionAnswersRepository = submissionAnswersRepository;
        }

        public async Task<TModel> New<TModel>(string examId, string authorId)
        {
            var exam = this.examsRepository
                .AllWithDeletedIncluding("Questions.Answers")
                .Where(x => x.Id == examId)
                .FirstOrDefault();

            var submission = new Submission
            {
                Questions = exam.Questions.Select(x => new SubmissionQuestion
                {
                    Content = x.Content,
                    Answers = x.Answers.Select(a => new SubmissionAnswer
                    {
                        Content = a.Content,
                        ExamAnswerId = a.Id,
                    }).ToList(),
                }).ToList(),
                ExamId = exam.Id,
                AuthorId = authorId,
            };

            await this.submissionsRepository.AddAsync(submission);
            await this.submissionsRepository.SaveChangesAsync();

            return AutoMapperConfig.MapperInstance.Map<TModel>(submission);
        }

        public async Task Complete(string submissionId, SubmissionAnswerInput[] answers)
        {
            var submissionAnswers = this.submissionAnswersRepository
                .All()
                .Where(x => answers.Select(a => a.Id).Contains(x.Id))
                .ToList();

            var submission = await this.submissionsRepository.FindByIdAsync(submissionId);

            submission.Ended = DateTime.UtcNow;

            foreach (var answer in submissionAnswers)
            {
                answer.IsCorrect = answers
                    .Where(x => x.Id == answer.Id)
                    .Select(x => x.IsCorrect)
                    .FirstOrDefault();
            }

            await this.submissionAnswersRepository.SaveChangesAsync();
            await this.submissionsRepository.SaveChangesAsync();
        }

        public TModel Get<TModel>(string id)
          => this.submissionsRepository
           .AllAsNoTracking()
           .Where(x => x.Id == id)
           .To<TModel>()
           .FirstOrDefault();
    }
}
