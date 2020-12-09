using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scio.Data.Migrations
{
    public partial class AddLecture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LectureId",
                table: "Resources",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ScheduledFor = table.Column<DateTime>(nullable: true),
                    CourseId = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lectures_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_LectureId",
                table: "Resources",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_AuthorId",
                table: "Lectures",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_CourseId",
                table: "Lectures",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_IsDeleted",
                table: "Lectures",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Lectures_LectureId",
                table: "Resources",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Lectures_LectureId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Resources_LectureId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "Resources");
        }
    }
}
