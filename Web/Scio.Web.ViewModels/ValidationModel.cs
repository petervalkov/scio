namespace Scio.Web.ViewModels
{
    using Scio.Data.Models;
    using Scio.Services.Mapping;

    public class ValidationModel : IMapFrom<ForumPost>
    {
        public string Id { get; set; }
    }
}
