namespace Scio.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    using Scio.Common;

    public class EditInputModel : IValidatableObject
    {
        public string Id { get; set; }

        [Display(Name = FieldName.Title)]
        [StringLength(
            Validation.MaxTitleLength,
            MinimumLength = Validation.MinStringLength,
            ErrorMessage = ErrorMessage.AllowedLengthRange)]
        public string Title { get; set; }

        public string Body { get; set; }

        public string AnswerBody { get; set; }

        public string QuestionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Id == null)
            {
                yield return new ValidationResult(
                    ErrorMessage.InvalidRequest,
                    new string[] { ErrorMessage.InvalidRequest });
            }

            if (this.QuestionId == null && this.AnswerBody == null)
            {
                if (this.Title == null)
                {
                    yield return new ValidationResult(
                        string.Format(ErrorMessage.RequiredField, FieldName.Title),
                        new string[] { nameof(this.Title) });
                }

                var body = Regex.Replace(this.Body ?? string.Empty, @"<(.|\n)*?>", string.Empty);
                if (string.IsNullOrWhiteSpace(body))
                {
                    yield return new ValidationResult(
                        string.Format(ErrorMessage.RequiredField, FieldName.PostBody),
                        new string[] { nameof(this.Body) });
                }
                else if (body.Length < Validation.MinStringLength)
                {
                    yield return new ValidationResult(
                        string.Format(ErrorMessage.MinLength, FieldName.PostBody, Validation.MinStringLength),
                        new string[] { nameof(this.Body) });
                }
            }
            else if (this.Title == null && this.Body == null)
            {
                var body = Regex.Replace(this.AnswerBody ?? string.Empty, @"<(.|\n)*?>", string.Empty);
                if (string.IsNullOrWhiteSpace(body))
                {
                    yield return new ValidationResult(
                        string.Format(ErrorMessage.RequiredField, FieldName.AnswerBody),
                        new string[] { nameof(this.AnswerBody) });
                }
                else if (body.Length < Validation.MinStringLength)
                {
                    yield return new ValidationResult(
                        string.Format(ErrorMessage.MinLength, FieldName.AnswerBody, Validation.MinStringLength),
                        new string[] { nameof(this.AnswerBody) });
                }
            }
            else
            {
                yield return new ValidationResult(
                    ErrorMessage.InvalidRequest,
                    new string[] { ErrorMessage.InvalidRequest });
            }
        }
    }
}
