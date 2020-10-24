namespace Scio.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class CreateInputModel : IValidatableObject
    {
        [MaxLength(400)]
        public string Title { get; set; }

        public string Body { get; set; }

        public string AnswerBody { get; set; }

        public string QuestionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var missingTitle = string.IsNullOrWhiteSpace(this.Title);

            if (this.QuestionId == null)
            {
                if (missingTitle)
                {
                    yield return new ValidationResult(ErrorMessage.RequiredField, new HashSet<string> { nameof(this.Title) });
                }

                if (string.IsNullOrWhiteSpace(this.Body))
                {
                    yield return new ValidationResult(ErrorMessage.RequiredField, new HashSet<string> { nameof(this.Body) });
                }
            }
            else if (missingTitle)
            {
                if (string.IsNullOrWhiteSpace(this.AnswerBody))
                {
                    yield return new ValidationResult(ErrorMessage.RequiredField, new HashSet<string> { nameof(this.AnswerBody) });
                }
            }
            else
            {
                yield return new ValidationResult(ErrorMessage.InvalidRequest); // If both QuestionId and Title are provided
            }
        }
    }
}
