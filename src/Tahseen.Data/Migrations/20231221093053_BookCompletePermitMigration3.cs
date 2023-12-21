using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tahseen.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookCompletePermitMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookCompletePermissions_Books_BookId",
                table: "bookCompletePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_bookCompletePermissions_LibraryBranches_LibraryBranchId",
                table: "bookCompletePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_bookCompletePermissions_Users_UserId",
                table: "bookCompletePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookCompletePermissions",
                table: "bookCompletePermissions");

            migrationBuilder.RenameTable(
                name: "bookCompletePermissions",
                newName: "BookCompletePermissions");

            migrationBuilder.RenameIndex(
                name: "IX_bookCompletePermissions_UserId",
                table: "BookCompletePermissions",
                newName: "IX_BookCompletePermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_bookCompletePermissions_LibraryBranchId",
                table: "BookCompletePermissions",
                newName: "IX_BookCompletePermissions_LibraryBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_bookCompletePermissions_BookId",
                table: "BookCompletePermissions",
                newName: "IX_BookCompletePermissions_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCompletePermissions",
                table: "BookCompletePermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCompletePermissions_Books_BookId",
                table: "BookCompletePermissions",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCompletePermissions_LibraryBranches_LibraryBranchId",
                table: "BookCompletePermissions",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCompletePermissions_Users_UserId",
                table: "BookCompletePermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCompletePermissions_Books_BookId",
                table: "BookCompletePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCompletePermissions_LibraryBranches_LibraryBranchId",
                table: "BookCompletePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCompletePermissions_Users_UserId",
                table: "BookCompletePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCompletePermissions",
                table: "BookCompletePermissions");

            migrationBuilder.RenameTable(
                name: "BookCompletePermissions",
                newName: "bookCompletePermissions");

            migrationBuilder.RenameIndex(
                name: "IX_BookCompletePermissions_UserId",
                table: "bookCompletePermissions",
                newName: "IX_bookCompletePermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCompletePermissions_LibraryBranchId",
                table: "bookCompletePermissions",
                newName: "IX_bookCompletePermissions_LibraryBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCompletePermissions_BookId",
                table: "bookCompletePermissions",
                newName: "IX_bookCompletePermissions_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookCompletePermissions",
                table: "bookCompletePermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookCompletePermissions_Books_BookId",
                table: "bookCompletePermissions",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookCompletePermissions_LibraryBranches_LibraryBranchId",
                table: "bookCompletePermissions",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookCompletePermissions_Users_UserId",
                table: "bookCompletePermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
