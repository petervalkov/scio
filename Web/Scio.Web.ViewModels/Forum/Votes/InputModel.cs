namespace Scio.Web.ViewModels.Forum.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class InputModel
    {
        [Required(ErrorMessage = "Invalid Post")]
        public string PostId { get; set; }

        [Required]
        [Range(-1, 1, ErrorMessage = "Invalid Vote")] // Add 0 check
        public int VoteValue { get; set; }

        public string UserId { get; set; }
    }
}
