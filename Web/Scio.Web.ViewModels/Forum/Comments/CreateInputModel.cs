namespace Scio.Web.ViewModels.Forum.Comments
{
    public class CreateInputModel
    {
        public string PostId { get; set; }

        public string ParentId { get; set; }

        public string Body { get; set; }
    }
}
