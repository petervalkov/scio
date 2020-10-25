namespace Scio.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Services.Mapping;
    using Scio.Web.Infrastructure.Utilities;
    using Scio.Web.ViewModels.Profile;

    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var userId = id ?? this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await this.userManager.Users
                .Where(u => u.Id == userId)
                .To<ViewModel>()
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return this.BadRequest();
            }

            return this.View(user);
        }

        public async Task<IActionResult> Edit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await this.userManager.Users
                .Where(u => u.Id == userId)
                .To<ViewModel>()
                .SingleOrDefaultAsync();

            return this.View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var viewModel = await this.userManager.Users
                    .Where(u => u.Id == userId)
                    .To<ViewModel>()
                    .SingleOrDefaultAsync();

                PropertyCopier<InputModel, ViewModel>.Copy(inputModel, viewModel);
                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            PropertyCopier<InputModel, ApplicationUser>.Copy(inputModel, user);

            if (inputModel.Image != null)
            {
                string url = await this.cloudinaryService
                    .UploadAsync(inputModel.Image, System.Guid.NewGuid().ToString());
                user.ImageUrl = url;
            }

            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.Details));
        }
    }
}
