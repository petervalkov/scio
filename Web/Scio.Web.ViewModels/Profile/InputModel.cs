namespace Scio.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Scio.Common;

    public class InputModel
    {
        [Display(Name = FieldName.DisplayName)]
        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        [StringLength(Validation.MaxNameLength, MinimumLength = Validation.MinNameLength, ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string DisplayName { get; set; }

        [Display(Name = FieldName.FullName)]
        [StringLength(Validation.MaxNameLength, MinimumLength = Validation.MinNameLength, ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string FullName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public IFormFile Image { get; set; }
    }
}
