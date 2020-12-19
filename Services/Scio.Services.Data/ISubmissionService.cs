namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Scio.Services.Data.DTOs;

    public interface ISubmissionService
    {
        Task<TModel> New<TModel>(string examId, string authorId);

        Task Complete(string submissionId, SubmissionAnswerInput[] answers);

        TModel Get<TModel>(string id);
    }
}
