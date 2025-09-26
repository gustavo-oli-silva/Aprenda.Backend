using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aprenda.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArchiveModelWithMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Archives");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Archives");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Archives",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalName",
                table: "Archives",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SizeInBytes",
                table: "Archives",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "StoredName",
                table: "Archives",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Archives_StoredName",
                table: "Archives",
                column: "StoredName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Archives_StoredName",
                table: "Archives");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Archives");

            migrationBuilder.DropColumn(
                name: "OriginalName",
                table: "Archives");

            migrationBuilder.DropColumn(
                name: "SizeInBytes",
                table: "Archives");

            migrationBuilder.DropColumn(
                name: "StoredName",
                table: "Archives");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Archives",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Archives",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
