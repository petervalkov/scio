namespace Scio.Web.Areas.Forum.Controllers
{
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CreateResponseModel>> Post(CreateInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var result = await this.commentService.Create<CreateResponseModel>(input.Body, null, input.PostId, userId);
            return result;
        }
    }
}
