namespace Scio.Web.ViewModels.Forum.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class EditInputModel
    {
        [Required]
        public string Id { get; set; }

        [MaxLength(400)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
