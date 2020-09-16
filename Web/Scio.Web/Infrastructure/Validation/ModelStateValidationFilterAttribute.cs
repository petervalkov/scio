namespace Scio.Web.Infrastructure.Validation
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ModelStateValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ViewResult
                {
                    ViewName = context.RouteData.Values["action"].ToString(),
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
