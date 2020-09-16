namespace Scio.Data.Common.Models
{
    public interface IOwnable<TKey>
    {
        TKey AuthorId { get; set; }
    }
}
