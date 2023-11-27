using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tahseen.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipStatus",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Librarians",
                newName: "Salt");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Librarians",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Librarians");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Librarians",
                newName: "UserName");

            migrationBuilder.AddColumn<int>(
                name: "MembershipStatus",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
