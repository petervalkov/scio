namespace Scio.Web.Areas.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Scio.Common;
    using Scio.Data.Models.Enums;
    using Scio.Services.Data;
    using Scio.Web.Areas.Api.Models;
    using Scio.Web.Infrastructure.Filters;
    using Scio.Web.ViewModels.Forum.Posts;

    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IForumVoteService forumVoteService;

        public VotesController(IForumVoteService forumVoteService)
        {
            this.forumVoteService = forumVoteService;
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult> Post(VoteInputModel input)
        {
            int data;
            string message;
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var vote = this.forumVoteService
                .Get<VoteModel>(input.PostId, userId);

            if (vote == null)
            {
                data = await this.forumVoteService
                    .CreateAsync(input.VoteValue, input.PostId, userId);
                message = vote.Value switch
                {
                    1 => string.Format(Message.SuccessfullVote, VoteType.Up.ToString().ToLower()),
                    -1 => string.Format(Message.SuccessfullVote, VoteType.Down.ToString().ToLower()),
                    _ => ErrorMessage.Default,
                };
            }
            else
            {
                if (input.VoteValue == vote.Value)
                {
                    return this.Ok(new { Message = string.Format(ErrorMessage.AlreadyVoted, vote.Type.ToLower()) });
                }

                data = await this.forumVoteService
                    .UpdateAsync(input.VoteValue, vote.Id);
                message = vote.Value switch
                {
                    1 => string.Format(Message.SuccessfullVote, VoteType.Down.ToString().ToLower()),
                    -1 => string.Format(Message.SuccessfullVote, VoteType.Up.ToString().ToLower()),
                    _ => ErrorMessage.Default,
                };
            }

            return this.Ok(new { Data = data, Message = message });
        }
    }
}
