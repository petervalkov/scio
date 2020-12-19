using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scio.Data.Migrations
{
    public partial class AddSubmissionQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "SubmissionAnswers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "SubmissionAnswers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmissionQuestionId",
                table: "SubmissionAnswers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubmissionQuestions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SubmissionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionQuestions_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAnswers_SubmissionQuestionId",
                table: "SubmissionAnswers",
                column: "SubmissionQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionQuestions_IsDeleted",
                table: "SubmissionQuestions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionQuestions_SubmissionId",
                table: "SubmissionQuestions",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionAnswers_SubmissionQuestions_SubmissionQuestionId",
                table: "SubmissionAnswers",
                column: "SubmissionQuestionId",
                principalTable: "SubmissionQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionAnswers_SubmissionQuestions_SubmissionQuestionId",
                table: "SubmissionAnswers");

            migrationBuilder.DropTable(
                name: "SubmissionQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionAnswers_SubmissionQuestionId",
                table: "SubmissionAnswers");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "SubmissionAnswers");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "SubmissionAnswers");

            migrationBuilder.DropColumn(
                name: "SubmissionQuestionId",
                table: "SubmissionAnswers");
        }
    }
}
