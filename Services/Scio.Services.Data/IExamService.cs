namespace Scio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Scio.Services.Data.DTOs;
    using Scio.Services.Data.Models.Exams;

    public interface IExamService
    {
        Task<string> CreateAsync(ExamInput exam);

        TModel Get<TModel>(string id);
    }
}
