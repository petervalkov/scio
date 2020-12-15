namespace Scio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Scio.Services.Data.DTOs;

    public interface IExamService
    {
        Task<string> CreateAsync(string title, DateTime opens, DateTime closes, int duration, ICollection<QuestionInput> questions, string courseId, string authorId);

        TModel Get<TModel>(string id);
    }
}
