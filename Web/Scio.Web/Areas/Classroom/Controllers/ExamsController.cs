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
    using Scio.Services.Data.Models.Exams;
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
            var exam = new ExamInput
            {
                Title = input.Title, // use automapper !!!
                Opens = input.Opens,
                Closes = input.Closes,
                Duration = input.Duration,
                AcceptAfterClosing = input.AcceptAfterClosing,
                AcceptExpiredTime = input.AcceptExpiredTime,
                RandomVariant = input.RandomVariant,
                QuestionsPerVariant = input.QuestionsPerVariant,
                AnswersPerQuestion = input.AnswersPerQuestion,
                CourseId = input.CourseId,
                AuthorId = userId,
                Questions = input.Question?.Select(q => new QuestionInput
                {
                    Content = q.Content,
                    Answers = q.Answers.Select(a => new AnswerInput
                    {
                        Content = a.Content,
                        Points = a.Points,
                        IsCorrect = a.IsCorrect,
                    }),
                }).ToList(),
            };

            var examId = await this.examService
                .CreateAsync(exam);

            return this.RedirectToAction(nameof(this.Details), new { id = examId });
        }

        [HttpGet]
        public IActionResult Details([Required] string id)
        {
            var viewModel = this.examService
                .Get<DetailsViewModel>(id);

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Start([Required] string id)
        {
            var viewModel = this.examService
                .Get<ExamStartViewModel>(id);

            return this.View(viewModel);
        }
    }
}
