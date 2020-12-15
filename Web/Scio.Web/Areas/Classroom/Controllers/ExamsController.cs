namespace Scio.Web.Areas.Classroom.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Scio.Services.Data;
    using Scio.Services.Data.DTOs;
    using Scio.Web.ViewModels.Classroom.Exams;

    [Area("Classroom")]
    public class ExamsController : Controller
    {
        private readonly IExamService examService;

        public ExamsController(IExamService examService)
        {
            this.examService = examService;
        }

        [HttpGet]
        public IActionResult New(string id)
        {
            this.ViewData["CourseId"] = id;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> New(InputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var questions = input.Question.Select(q => new QuestionInput
            {
                Content = q.Content,
                Answers = q.Answers.Select(a => new AnswerInput
                {
                    Content = a.Content,
                    Points = a.Points,
                    IsCorrect = a.IsCorrect,
                }),
            }).ToList();

            // FIX
            var openWindow = input.OpenInterval.Split("-");
            var opens = DateTime.ParseExact(openWindow[0].Trim(), "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            var closes = DateTime.ParseExact(openWindow[1].Trim(), "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);

            var examId = await this.examService
                .CreateAsync(input.Title, opens, closes, input.Duration, questions, input.CourseId, userId);

            return this.RedirectToAction(nameof(this.Details), new { id = examId });
        }

        [HttpGet]
        public IActionResult Details([Required] string id)
        {
            var viewModel = this.examService
                .Get<DetailsViewModel>(id);

            return this.View(viewModel);
        }
    }
}
