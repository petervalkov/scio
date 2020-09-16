namespace Scio.Web.Areas.Forum.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.Infrastructure.Validation;
    using Scio.Web.ViewModels.Forum.Answers;
    using Scio.Web.ViewModels.Forum.Common;

    [Area("Forum")]
    public class AnswersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthorizationService authorizationService;
        private readonly IAnswerService answerService;

        public AnswersController(
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService,
            IAnswerService answerService)
        {
            this.userManager = userManager;
            this.authorizationService = authorizationService;
            this.answerService = answerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                await this.answerService.CreateAsync(input.Content, input.QuestionId, user.Id);
            }

            return this.Redirect($"/Forum/Questions/Details/{input.QuestionId}");
        }

        [IdentifierValidationFilter]
        public async Task<IActionResult> Edit([Required]string id)
        {
            var answer = this.answerService.GetById<ViewModels.Forum.Answers.EditViewModel>(id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, answer, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            return this.View(answer);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Edit(ViewModels.Forum.Answers.EditInputModel input)
        {
            var answer = this.answerService.GetById<ViewModels.Forum.Answers.EditInputModel>(input.Id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, answer, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            await this.answerService.EditAsync(input.Id, input.Content);
            return this.Redirect($"/Forum/Questions/Details/{answer.QuestionId}");
        }

        [IdentifierValidationFilter]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var answer = this.answerService.GetById<ResourceOwnerValidationModel>(id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, answer, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            await this.answerService.DeleteAsync(id);
            return this.Redirect($"/Forum/Questions/Details/{answer.QuestionId}");
        }
    }
}
