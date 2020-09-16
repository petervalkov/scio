namespace Scio.Web.Areas.Forum.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.Infrastructure.Authorization;
    using Scio.Web.Infrastructure.Validation;
    using Scio.Web.ViewModels.Forum.Common;
    using Scio.Web.ViewModels.Forum.Questions;

    [Area("Forum")]
    public class QuestionsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthorizationService authorizationService;
        private readonly IQuestionService questionService;

        public QuestionsController(
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService,
            IQuestionService questionService)
        {
            this.userManager = userManager;
            this.authorizationService = authorizationService;
            this.questionService = questionService;
        }

        public IActionResult Create(CreateViewModel viewModel)
        {
            return this.View(viewModel);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Create(CreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var questionId = await this.questionService.CreateAsync(input.Title, input.Content, user.Id);

            if (questionId != null)
            {
                return this.Redirect($"/Forum/Questions/Details/{questionId}");
            }

            return this.View(input);
        }

        [AllowAnonymous]
        [IdentifierValidationFilter]
        public IActionResult Details([Required]string id)
        {
            var viewModel = this.questionService.GetById<DetailsViewModel>(id);
            return this.View(viewModel);
        }

        [IdentifierValidationFilter]
        public async Task<IActionResult> Edit([Required]string id)
        {
            var question = this.questionService.GetById<EditViewModel>(id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, question, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            return this.View(question);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Edit(EditInputModel input)
        {
            var question = this.questionService.GetById<EditViewModel>(input.Id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, question, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            await this.questionService.EditAsync(input.Id, input.Title, input.Content);
            return this.Redirect($"/Forum/Questions/Details/{input.Id}");
        }

        [IdentifierValidationFilter]
        public async Task<IActionResult> Delete([Required]string id)
        {
            var question = this.questionService.GetById<ResourceOwnerValidationModel>(id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, question, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            await this.questionService.DeleteAsync(id);
            return this.Redirect($"/Forum");
        }
    }
}
