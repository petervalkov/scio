namespace Scio.Web.ViewModels.Forum.Questions
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAnswerInputModel
    {
        [Required]
        public string Content { get; set; }

        public string QuestionId { get; set; }
    }
}
