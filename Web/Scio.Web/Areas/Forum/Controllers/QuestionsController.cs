namespace Scio.Web.Areas.Forum.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Forum.Questions;

    [Area("Forum")]
    public class QuestionsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IQuestionService questionService;

        public QuestionsController(
            UserManager<ApplicationUser> userManager,
            IQuestionService questionService)
        {
            this.userManager = userManager;
            this.questionService = questionService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var questionId = await this.questionService.CreateAsync(input.Title, input.Content, user.Id);

                if (questionId != null)
                {
                    return this.Redirect($"/Forum/Questions/Details/{questionId}");
                }
            }

            return this.View(input);
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.questionService.GetById<QuestionDetailsViewModel>(id);
            return this.View(viewModel);
        }
    }
}
