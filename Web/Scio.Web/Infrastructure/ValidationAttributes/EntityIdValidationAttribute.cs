namespace Scio.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using Scio.Services.Data;
    using Scio.Web.ViewModels;

    public class EntityIdValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var some = validationContext
                    .GetService(typeof(IForumPostService)) as IForumPostService;

                var validId = some.Get<ValidationModel>(value.ToString());
                if (validId == null)
                {
                    return new ValidationResult(
                       Common.ErrorMessage.InvalidRequest,
                       new string[] { Common.ErrorMessage.InvalidRequest });
                }

                return ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}
