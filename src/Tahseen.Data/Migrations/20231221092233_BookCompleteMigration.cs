using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tahseen.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookCompleteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookImage",
                table: "CompletedBooks");

            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "CompletedBooks");

            migrationBuilder.DropColumn(
                name: "LibraryBranchName",
                table: "CompletedBooks");

            migrationBuilder.AddColumn<long>(
                name: "LibraryBranchId",
                table: "CompletedBooks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CompletedBooks_LibraryBranchId",
                table: "CompletedBooks",
                column: "LibraryBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedBooks_LibraryBranches_LibraryBranchId",
                table: "CompletedBooks",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedBooks_LibraryBranches_LibraryBranchId",
                table: "CompletedBooks");

            migrationBuilder.DropIndex(
                name: "IX_CompletedBooks_LibraryBranchId",
                table: "CompletedBooks");

            migrationBuilder.DropColumn(
                name: "LibraryBranchId",
                table: "CompletedBooks");

            migrationBuilder.AddColumn<string>(
                name: "BookImage",
                table: "CompletedBooks",
                type: "text",
                nullable: true);

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
    }
}
