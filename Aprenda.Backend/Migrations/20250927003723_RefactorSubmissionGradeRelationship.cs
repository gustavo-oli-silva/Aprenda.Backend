using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aprenda.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RefactorSubmissionGradeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Grades_GradeId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_GradeId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Submissions");

            migrationBuilder.AddColumn<long>(
                name: "SubmissionId",
                table: "Grades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SubmissionId",
                table: "Grades",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Submissions_SubmissionId",
                table: "Grades",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Submissions_SubmissionId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_SubmissionId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "Grades");

            migrationBuilder.AddColumn<long>(
                name: "GradeId",
                table: "Submissions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_GradeId",
                table: "Submissions",
                column: "GradeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Grades_GradeId",
                table: "Submissions",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
