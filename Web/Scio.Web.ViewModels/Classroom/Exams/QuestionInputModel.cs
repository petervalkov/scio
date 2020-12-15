namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System.Collections.Generic;

    public class QuestionInputModel
    {
        public string Content { get; set; }

        public int Type { get; set; }

        public ICollection<AnswerInputModel> Answers { get; set; }
    }
}
