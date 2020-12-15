namespace Scio.Web.ViewModels.Classroom.Exams
{
    using System;
    using System.Collections.Generic;

    public class InputModel
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public string OpenInterval { get; set; }

        // public DateTime Closes { get; set; }
        public string CourseId { get; set; }

        public ICollection<QuestionInputModel> Question { get; set; }
    }
}
