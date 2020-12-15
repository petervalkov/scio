namespace Scio.Services.Data.DTOs
{
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class QuestionInput : IMapTo<ExamQuestion>
    {
        public string Content { get; set; }

        public IEnumerable<AnswerInput> Answers { get; set; }
    }
}
