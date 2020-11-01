namespace Scio.Web.Areas.Forum.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Scio.Common;
    using Scio.Services.Data;
    using Scio.Web.ViewModels;
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
            this.forumVoteService = forumVoteService;
            this.forumPostService = forumPostService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel>> Post(InputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = this.forumPostService.SearchForVote<PostValidationModel>(input.PostId, userId);

            if (post == null || userId == null)
            {
                return this.BadRequest(new ApiResponseModel { Message = ErrorMessage.Default });
            }

            if (post.Vote != null)
            {
                if (input.VoteValue == 0)
                {
                    return this.BadRequest(new ApiResponseModel { Message = ErrorMessage.Default });
                }

                if (input.VoteValue == post.Vote.Value)
                {
                    return this.BadRequest(new ApiResponseModel { Message = string.Format(ErrorMessage.AlreadyVoted, post.Vote.Type.ToLower()) });
                }

                var voteType = post.Vote.Type.ToLower() == "up" ? "down" : "up";
                return new ApiResponseModel
                {
                    Data = await this.forumVoteService.UpdateVoteAsync(input.VoteValue, post.Vote.Id),
                    Message = string.Format(Message.SuccessfullVote, voteType),
                };
            }
            else
            {
                return new ApiResponseModel
                {
                    Data = await this.forumVoteService.CreateVoteAsync(input.VoteValue, input.PostId, userId),
                    Message = string.Format(Message.SuccessfullVote, input.VoteValue == 1 ? "up" : "down"),
                };
            }
        }
    }
}
