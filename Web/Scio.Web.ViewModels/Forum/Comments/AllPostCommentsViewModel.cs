namespace Scio.Web.ViewModels.Forum.Comments
{
    using System.Collections.Generic;

    public class AllPostCommentsViewModel
    {
        public IEnumerable<PostCommentsViewModel> Comments { get; set; }
    }
}
