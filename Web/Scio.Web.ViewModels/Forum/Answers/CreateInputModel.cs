namespace Scio.Web.ViewModels.Forum.Answers
{
    using System.ComponentModel.DataAnnotations;

    public class CreateInputModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string QuestionId { get; set; }
    }
}
