using Microsoft.EntityFrameworkCore.Migrations;

namespace Scio.Data.Migrations
{
    public partial class AddCourseTypeToCourseUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "CourseUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "CourseUsers");
        }
    }
}
