namespace Scio.Web.Areas.Forum.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Forum.Comments;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IForumCommentService commentService;

        public CommentsController(
            UserManager<ApplicationUser> userManager,
            IForumCommentService commentService)
        {
            this.userManager = userManager;
            this.commentService = commentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<AllPostCommentsViewModel> Get(string postId)
        {
            if (postId == null)
            {
                return this.BadRequest();
            }

            var result = this.commentService.GetAllByParentId<PostCommentsViewModel>(postId, null);

            if (result == null)
            {
                return this.BadRequest();
            }

            return new AllPostCommentsViewModel { Comments = result };
        }

        [HttpPost]
        public async Task<ActionResult<PostCommentsViewModel>> Post(InputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var result = await this.commentService.Create<PostCommentsViewModel>(input.Body, null, input.PostId, userId);
            return result;
        }
    }
}
