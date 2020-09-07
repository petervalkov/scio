namespace Scio.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Scio.Data.Common.Models;

    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Content { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public Question Question { get; set; }

        public string QuestionId { get; set; }
    }
}
