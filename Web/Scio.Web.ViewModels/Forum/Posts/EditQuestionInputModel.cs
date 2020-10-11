namespace Scio.Web.ViewModels.Forum.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class EditQuestionInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
