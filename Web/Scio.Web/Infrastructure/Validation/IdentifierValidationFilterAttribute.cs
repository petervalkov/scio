namespace Scio.Web.Infrastructure.Validation
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class IdentifierValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new NotFoundResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
