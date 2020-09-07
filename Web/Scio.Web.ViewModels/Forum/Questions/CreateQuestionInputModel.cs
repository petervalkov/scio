﻿namespace Scio.Web.ViewModels.Forum.Questions
{
    using System.ComponentModel.DataAnnotations;

    public class CreateQuestionInputModel
    {
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
