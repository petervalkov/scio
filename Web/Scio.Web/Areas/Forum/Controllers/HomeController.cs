namespace Scio.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Services.Data;
    using Scio.Web.ViewModels.Forum.Home;

    [Area("Forum")]
    public class HomeController : Controller
    {
        private readonly IQuestionService questionService;

        public HomeController(
            IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var viewModel = this.questionService.GetAll<IndexViewModel>();
            return this.View(viewModel);
        }
    }
}
