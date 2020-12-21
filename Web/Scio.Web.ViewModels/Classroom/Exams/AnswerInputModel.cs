namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class AnswerInputModel : IMapTo<ExamAnswer>
    {
        [Required]
        [MaxLength(400)]
        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public int Points { get; set; }
    }
}
