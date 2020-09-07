namespace Scio.Web.ViewModels.Forum.Home
{
    using System;

    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class IndexQuestionsViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AnswersCount { get; set; }

        public string AuthorId { get; set; }

        public string AuthorEmail { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
