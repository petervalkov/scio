namespace Scio.Web.Areas.Forum.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Common;
    using Scio.Services.Data;
    using Scio.Web.Infrastructure.Filters;
    using Scio.Web.ViewModels;
    using Scio.Web.ViewModels.Forum.Comments;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IForumCommentService commentService;
        private readonly IForumPostService forumPostService;

        public CommentsController(
            IForumCommentService commentService,
            IForumPostService forumPostService)
        {
            this.commentService = commentService;
            this.forumPostService = forumPostService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ModelStateValidationFilter]
        public ActionResult<AllPostCommentsViewModel> Get([Required] string postId)
        {
            var result = this.commentService
                .GetAllByParentId<PostCommentsViewModel>(postId, null);

            if (result == null)
            {
                return this.BadRequest(new ApiResponseModel { Message = ErrorMessage.Default });
            }

            return this.Ok(new ApiResponseModel { Data = result, Message = Message.SuccessDefault });
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult<PostCommentsViewModel>> Post(InputModel input)
        {
            if (this.forumPostService.Get<ValidationModel>(input.PostId) == null)
            {
                return this.BadRequest(new ApiResponseModel { Message = ErrorMessage.Default });
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.commentService
                .Create<PostCommentsViewModel>(input.Body, null, input.PostId, userId);

            if (result == null)
            {
                return this.BadRequest(new ApiResponseModel { Message = ErrorMessage.Default });
            }

            return this.Ok(new ApiResponseModel { Data = result, Message = Message.SuccessDefault });
        }
    }
}
