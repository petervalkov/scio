namespace Scio.Web.Areas.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Web.Infrastructure.ValidationAttributes;

    public class VoteInputModel
    {
        [PostId]
        [Required]
        public string PostId { get; set; }

        [Required]
        [RegularExpression("^-?[1]$")]
        public int VoteValue { get; set; }
    }
}
