namespace Scio.Web.ViewModels.Forum.Comments
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;

    public class InputModel
    {
        public string PostId { get; set; }

        [Required]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = Validation.MinStringLength,
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Body { get; set; }
    }
}
