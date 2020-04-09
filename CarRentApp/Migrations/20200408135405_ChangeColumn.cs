using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentApp.Migrations
{
    public partial class ChangeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CarTypes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarTypes");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "CarTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
