namespace Scio.Web.Areas.Forum.Models.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using System.Web;

    using Scio.Common;
    using Scio.Web.Infrastructure.ValidationAttributes;

    public class CreateInputModel : IValidatableObject
    {
        [Display(Name = FieldName.Title)]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = Validation.MinStringLength,
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Title { get; set; }

        public string PostBody { get; set; }

        [EntityIdValidation]
        public string QuestionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var body = Regex.Replace(this.PostBody ?? string.Empty, @"<(.|\n)*?>", string.Empty);
            var missingTitle = string.IsNullOrWhiteSpace(this.Title);
            var missingQuestionId = string.IsNullOrWhiteSpace(this.QuestionId);

            if (string.IsNullOrWhiteSpace(body))
            {
                yield return new ValidationResult(
                    string.Format(ErrorMessage.RequiredField, FieldName.PostBody),
                    new string[] { nameof(this.PostBody) });
            }
            else if (body.Length < Validation.MinStringLength)
            {
                yield return new ValidationResult(
                    string.Format(ErrorMessage.MinLength, FieldName.PostBody, Validation.MinStringLength),
                    new string[] { nameof(this.PostBody) });
            }

            if (!missingTitle && !missingQuestionId)
            {
                yield return new ValidationResult(
                    ErrorMessage.InvalidRequest,
                    new string[] { ErrorMessage.InvalidRequest });
            }
            else if (missingTitle && missingQuestionId)
            {
                yield return new ValidationResult(
                        string.Format(ErrorMessage.RequiredField, FieldName.Title),
                        new string[] { nameof(this.Title) });
            }
        }
    }
}
