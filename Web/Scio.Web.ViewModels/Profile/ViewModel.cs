namespace Scio.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ViewModel : IMapFrom<ApplicationUser>
    {
        [Display(Name = FieldName.DisplayName)]
        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        [StringLength(Validation.MaxNameLength, MinimumLength = Validation.MinNameLength, ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string DisplayName { get; set; }

        [Display(Name = FieldName.FullName)]
        [StringLength(Validation.MaxNameLength, MinimumLength = Validation.MinNameLength, ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string FullName { get; set; }

        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public int ForumPostsCount { get; set; }

        public int ForumCommentsCount { get; set; }
    }
}
