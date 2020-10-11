namespace Scio.Web.ViewModels.Forum.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAnswerInputModel
    {
        [Required]
        public string Body { get; set; }

        [Required]
        public string QuestionId { get; set; }
    }
}
