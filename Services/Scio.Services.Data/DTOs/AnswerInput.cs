namespace Scio.Services.Data.DTOs
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class AnswerInput : IMapTo<ExamAnswer>
    {
        public string Content { get; set; }

        public int Points { get; set; }

        public bool IsCorrect { get; set; }
    }
}
