namespace Scio.Web.ViewModels.Classroom.Exams
{
    public class SubmissionInputModel
    {
        public string SubmissionId { get; set; }

        public SubmissionQuestionInputModel[] Questions { get; set; }
    }
}
