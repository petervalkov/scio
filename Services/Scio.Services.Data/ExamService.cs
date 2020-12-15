namespace Scio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Services.Data.DTOs;
    using Scio.Services.Mapping;

    public class ExamService : IExamService
    {
        private readonly IDeletableEntityRepository<Exam> examsRepository;

        public ExamService(IDeletableEntityRepository<Exam> examsRepository)
        {
            this.examsRepository = examsRepository;
        }

        public async Task<string> CreateAsync(string title, DateTime opens, DateTime closes, int duration, ICollection<QuestionInput> questions, string courseId, string authorId)
        {
            var exam = new Exam
            {
                Title = title,
                Opens = opens,
                Closes = closes,
                Duration = duration,
                CourseId = courseId,
                AuthorId = authorId,
                Questions = questions.AsQueryable().To<ExamQuestion>().ToArray(),
            };

            await this.examsRepository.AddAsync(exam);
            await this.examsRepository.SaveChangesAsync();

            return exam.Id;
        }

        public TModel Get<TModel>(string id)
           => this.examsRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<TModel>()
            .FirstOrDefault();
    }
}
