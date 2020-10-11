namespace Scio.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Forum")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index() => this.Redirect("/Forum/Posts/All");
    }
}
