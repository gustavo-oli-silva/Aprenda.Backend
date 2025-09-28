using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aprenda.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddInviteCodeToClassroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InviteCode",
                table: "Classrooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE Classrooms SET InviteCode = NEWID() WHERE InviteCode = ''");
            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_InviteCode",
                table: "Classrooms",
                column: "InviteCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classrooms_InviteCode",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "InviteCode",
                table: "Classrooms");
        }
    }
}
