using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tahseen.Data.Migrations
{
    /// <inheritdoc />
    public partial class BorrowedBookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookImage",
                table: "BorrowedBooks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryBranchName",
                table: "BorrowedBooks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookImage",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "LibraryBranchName",
                table: "BorrowedBooks");
        }
    }
}
