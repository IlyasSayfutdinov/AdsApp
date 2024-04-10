using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.Migrations
{
    /// <inheritdoc />
    public partial class categorymissed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Advertisements");
        }
    }
}
