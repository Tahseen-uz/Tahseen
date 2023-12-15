using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tahseen.Data.Migrations
{
    /// <inheritdoc />
    public partial class completedBookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
                table: "CompletedBooks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryBranchName",
                table: "CompletedBooks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "CompletedBooks");

            migrationBuilder.DropColumn(
                name: "LibraryBranchName",
                table: "CompletedBooks");
        }
    }
}
