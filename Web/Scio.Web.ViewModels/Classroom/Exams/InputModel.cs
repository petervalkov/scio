namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InputModel
    {
        [Required]
        [MaxLength(400)]
        public string Title { get; set; }

        public int? Duration { get; set; }

        public DateTime? Opens { get; set; }

        public DateTime? Closes { get; set; }

        [Required]
        public string CourseId { get; set; }

        public string CourseTitle { get; set; }

        public bool AcceptAfterClosing { get; set; }

        public bool AcceptExpiredTime { get; set; }

        public bool RandomVariant { get; set; }

        public int QuestionsPerVariant { get; set; }

        public int AnswersPerQuestion { get; set; }

        public ICollection<QuestionInputModel> Question { get; set; }
    }
}
