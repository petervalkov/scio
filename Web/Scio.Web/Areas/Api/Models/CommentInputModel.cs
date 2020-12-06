namespace Scio.Web.Areas.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;
    using Scio.Web.Infrastructure.ValidationAttributes;

    public class CommentInputModel
    {
        [Required]
        [PostId]
        public string PostId { get; set; }

        [Required]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = Validation.MinStringLength,
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Body { get; set; }
    }
}
