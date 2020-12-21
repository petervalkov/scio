using Microsoft.EntityFrameworkCore.Migrations;

namespace Scio.Data.Migrations
{
    public partial class AddSettingsToExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptAfterClosing",
                table: "Exams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptExpiredTime",
                table: "Exams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AnswersPerQuestion",
                table: "Exams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionsPerVariant",
                table: "Exams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RandomVariant",
                table: "Exams",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptAfterClosing",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "AcceptExpiredTime",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "AnswersPerQuestion",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "QuestionsPerVariant",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "RandomVariant",
                table: "Exams");
        }
    }
}
