namespace Scio.Web.Areas.Forum.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Forum.Questions;

    [Area("Forum")]
    public class AnswersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAnswerService answerService;

        public AnswersController(
            UserManager<ApplicationUser> userManager,
            IAnswerService answerService)
        {
            this.userManager = userManager;
            this.answerService = answerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnswerInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                await this.answerService.CreateAsync(input.Content, input.QuestionId, user.Id);
            }

            return this.Redirect($"/Forum/Questions/Details/{input.QuestionId}");
        }
    }
}
