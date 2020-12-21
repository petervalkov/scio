namespace Scio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Services.Data.DTOs;
    using Scio.Services.Data.Models.Exams;
    using Scio.Services.Mapping;

    public class ExamService : IExamService
    {
        private readonly IDeletableEntityRepository<Exam> examsRepository;
        private readonly INotificationService notificationService;

        public ExamService(
            IDeletableEntityRepository<Exam> examsRepository,
            INotificationService notificationService)
        {
            this.examsRepository = examsRepository;
            this.notificationService = notificationService;
        }

        public async Task<string> CreateAsync(ExamInput examInput)
        {
            var exam = AutoMapperConfig.MapperInstance.Map<Exam>(examInput);

            await this.examsRepository.AddAsync(exam);
            await this.examsRepository.SaveChangesAsync();

            await this.notificationService
                .CreateAsync("created new exam", $"classroom/exams/start/{exam.Id}", 1, null, exam.AuthorId, exam.CourseId);

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
