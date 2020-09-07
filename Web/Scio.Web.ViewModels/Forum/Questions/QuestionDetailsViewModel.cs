namespace Scio.Web.ViewModels.Forum.Questions
{
    using System;
    using System.Collections.Generic;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class QuestionDetailsViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorEmail { get; set; }

        public IEnumerable<QuestionAnswerViewModel> Answers { get; set; }
    }
}
