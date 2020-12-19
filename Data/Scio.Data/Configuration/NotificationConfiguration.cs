namespace Scio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Scio.Data.Models;

    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> notification)
        {
            notification.HasOne(n => n.Author)
                .WithMany(a => a.Activity)
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            notification.HasOne(n => n.Recipient)
                .WithMany(a => a.Notifications)
                .HasForeignKey(n => n.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
