namespace Scio.Web.Areas.Api.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Services.Data;
    using Scio.Web.Areas.Api.Models;
    using Scio.Web.Infrastructure.Filters;
    using Scio.Web.Infrastructure.ValidationAttributes;
    using Scio.Web.ViewModels.Forum.Posts;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IForumCommentService commentService;

        public CommentsController(IForumCommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ModelStateValidationFilter]
        public ActionResult Get([Required][PostId] string postId)
        {
            var result = this.commentService
                .GetAllByParentId<CommentModel>(postId, null);

            if (result == null)
            {
                return this.BadRequest();
            }

            return this.Ok(result);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult> Post(CommentInputModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.commentService
                .Create<CommentModel>(input.Body, null, input.PostId, userId);

            if (result == null)
            {
                return this.BadRequest();
            }

            return this.Ok(result);
        }
    }
}
