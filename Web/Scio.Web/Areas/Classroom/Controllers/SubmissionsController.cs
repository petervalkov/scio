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
    public class SubmissionsController : Controller
    {
        private readonly IExamService examService;
        private readonly ISubmissionService submissionService;

        public SubmissionsController(
            IExamService examService,
            ISubmissionService submissionService)
        {
            this.examService = examService;
            this.submissionService = submissionService;
        }

        [HttpGet]
        public async Task<IActionResult> New([Required] string id)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await this.submissionService
                .New<SubmissionViewModel>(id, userId);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> New(SubmissionInputModel input)
        {
            var answers = input.Questions
                .SelectMany(x => x.Answers.Select(a => new SubmissionAnswerInput
                {
                    Id = a.Id,
                    IsCorrect = a.IsCorrect,
                })).ToArray();

            await this.submissionService
                .Complete(input.SubmissionId, answers);

            return this.RedirectToAction(nameof(this.Details), new { id = input.SubmissionId });
        }

        [HttpGet]
        [Route("Classroom/Submissions/{id}")]
        public IActionResult Details([Required] string id)
        {
            var resut = this.submissionService
                .Get<ResultViewModel>(id);

            return this.View(resut);
        }
    }
}
