namespace Scio.Web.ViewModels.Forum.Questions
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class QuestionAnswerViewModel : IMapFrom<Answer>
    {
        public string Content { get; set; }

        public string AuthorEmail { get; set; }

        public string AuthorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
