namespace Scio.Web.ViewModels.Forum.Votes
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;

    public class InputModel
    {
        [Required(ErrorMessage = ErrorMessage.Default)]
        public string PostId { get; set; }

        [Required]
        [Range(-1, 1, ErrorMessage = ErrorMessage.Default)]
        public int VoteValue { get; set; }
    }
}
