namespace Scio.Web.ViewModels.Forum.Home
{
    using System;

    using AutoMapper;

    using Scio.Data.Models;
    using Scio.Services.Mapping;
    using Scio.Web.ViewModels.Forum.Questions;

    public class IndexViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int AnswersCount { get; set; }

        public string AuthorId { get; set; }

        public string AuthorEmail { get; set; }
    }
}
