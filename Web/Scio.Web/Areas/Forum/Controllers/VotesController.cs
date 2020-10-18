namespace Scio.Web.Areas.Forum.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Forum.Votes;

    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IForumVoteService forumVoteService;
        private readonly IForumPostService forumPostService;

        public VotesController(
            IForumVoteService forumVoteService,
            IForumPostService forumPostService)
        {
            // this.userManager = userManager;
            this.forumVoteService = forumVoteService;
            this.forumPostService = forumPostService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> Post(InputModel input) // Fix messages
        {
            var post = this.forumPostService.SearchForVote<PostValidationModel>(input.PostId, input.UserId);

            if (post == null)
            {
                this.ModelState.AddModelError(nameof(input.PostId), "Invalid Post");
                return this.BadRequest(this.ModelState);
            }

            if (post.Vote != null)
            {
                if (input.VoteValue == 0)
                {
                    this.ModelState.AddModelError(nameof(input.VoteValue), $"Invalid Vote");
                    return this.BadRequest(this.ModelState);
                }

                if (input.VoteValue == post.Vote.Value)
                {
                    this.ModelState.AddModelError(nameof(input.VoteValue), $"You have already {post.Vote.Type.ToLower()}voted this question");
                    return this.BadRequest(this.ModelState);
                }

                return new ResponseModel
                {
                    VotesSum = await this.forumVoteService.UpdateVoteAsync(input.VoteValue, post.Vote.Id),
                    Message = $"You have successfully {(post.Vote.Type.ToLower() == "up" ? "down" : "up")}voted this question (update)",
                };
            }
            else
            {
                return new ResponseModel
                {
                    VotesSum = await this.forumVoteService.CreateVoteAsync(input.VoteValue, input.PostId, input.UserId),
                    Message = $"You have successfully {post.Vote.Type.ToLower()}voted this question (create)",
                };
            }
        }
    }
}
