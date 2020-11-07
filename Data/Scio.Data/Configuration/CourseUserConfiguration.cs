namespace Scio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Scio.Data.Models;

    public class CourseUserConfiguration : IEntityTypeConfiguration<CourseUser>
    {
        public void Configure(EntityTypeBuilder<CourseUser> courseUser)
        {
            courseUser.HasKey(cu => new { cu.CourseId, cu.UserId });
        }
    }
}
