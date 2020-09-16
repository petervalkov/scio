namespace Scio.Web.ViewModels.Forum.Questions
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string AuthorEmail { get; set; }

        public string AuthorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
