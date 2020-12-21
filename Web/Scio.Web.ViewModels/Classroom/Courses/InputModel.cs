namespace Scio.Web.ViewModels.Classroom.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    using Scio.Common;

    public class InputModel
    {
        [Required]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = Validation.MinStringLength,
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = 50, // CHECK
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Description { get; set; }

        [Range(0, 1, ErrorMessage = ErrorMessage.InvalidCourseType)]
        public int Type { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var description = Regex.Replace(this.Description ?? string.Empty, @"<(.|\n)*?>", string.Empty);
        //    if (string.IsNullOrWhiteSpace(description))
        //    {
        //        yield return new ValidationResult(
        //            string.Format(ErrorMessage.RequiredField, nameof(this.Description)),
        //            new[] { nameof(this.Description) });
        //    }
        //    else if (description.Length < Validation.MinStringLength)
        //    {
        //        yield return new ValidationResult(
        //            string.Format(ErrorMessage.MinLength, nameof(this.Description), Validation.MinStringLength),
        //            new[] { nameof(this.Description) });
        //    }
        //}
    }
}
