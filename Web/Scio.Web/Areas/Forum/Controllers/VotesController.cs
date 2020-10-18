namespace Scio.Web.Areas.Forum.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Forum.Votes;

    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IForumVoteService forumVoteService;
        private readonly IForumPostService forumPostService;

        public VotesController(
            UserManager<ApplicationUser> userManager,
            IForumVoteService forumVoteService,
            IForumPostService forumPostService)
        {
            this.userManager = userManager;
            this.forumVoteService = forumVoteService;
            this.forumPostService = forumPostService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Post(InputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var post = this.forumPostService.SearchForVote<PostValidationModel>(input.PostId, userId);

            if (post == null || userId == null)
            {
                this.ModelState.AddModelError(nameof(ErrorMessage), ErrorMessage.Default);
                return this.BadRequest(this.ModelState);
            }

            if (post.Vote != null)
            {
                if (input.VoteValue == 0)
                {
                    this.ModelState.AddModelError(nameof(ErrorMessage), ErrorMessage.Default);
                    return this.BadRequest(this.ModelState);
                }

                if (input.VoteValue == post.Vote.Value)
                {
                    this.ModelState.AddModelError(nameof(ErrorMessage), string.Format(ErrorMessage.AlreadyVoted, post.Vote.Type.ToLower()));
                    return this.BadRequest(this.ModelState);
                }

                var voteType = post.Vote.Type.ToLower() == "up" ? "down" : "up";
                return new ResponseModel
                {
                    VotesSum = await this.forumVoteService.UpdateVoteAsync(input.VoteValue, post.Vote.Id),
                    Message = string.Format(Message.SuccessfullVote, voteType),
                };
            }
            else
            {
                return new ResponseModel
                {
                    VotesSum = await this.forumVoteService.CreateVoteAsync(input.VoteValue, input.PostId, userId),
                    Message = string.Format(Message.SuccessfullVote, input.VoteValue == 1 ? "up" : "down"),
                };
            }
        }
    }
}
