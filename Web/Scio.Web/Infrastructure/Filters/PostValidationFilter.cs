namespace Scio.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Scio.Common;
    using Scio.Services.Data;
    using Scio.Web.Areas.Forum.Models.Posts;
    using Scio.Web.ViewModels.Forum.Posts;

    public class PostValidationFilter : IActionFilter
    {
        private readonly IForumPostService forumPostService;

        public PostValidationFilter(
            IForumPostService forumPostService)
        {
            this.forumPostService = forumPostService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var questionId = context.ActionArguments.TryGetValue("input", out object input)
                    ? (input as CreateInputModel)?.QuestionId
                    : null;

                if (context.ModelState.ContainsKey(ErrorMessage.InvalidRequest))
                {
                    context.Result = new BadRequestResult();
                }
                else if (questionId == null)
                {
                    context.Result = new ViewResult
                    {
                        ViewName = context.RouteData.Values["action"].ToString(),
                    };
                }
                else
                {
                    context.Result = new ViewResult
                    {
                        ViewName = "Details",
                        ViewData = new ViewDataDictionary((context.Controller as Controller).ViewData)
                        {
                            Model = this.forumPostService.Get<DetailsViewModel>(questionId),
                        },
                    };
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
