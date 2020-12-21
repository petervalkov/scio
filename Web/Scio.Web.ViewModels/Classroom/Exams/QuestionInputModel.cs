namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;

    public class QuestionInputModel
    {
        [Required]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = Validation.MinStringLength,
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Content { get; set; }

        public int Type { get; set; }

        public ICollection<AnswerInputModel> Answers { get; set; }
    }
}
