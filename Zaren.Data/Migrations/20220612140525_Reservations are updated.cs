using Microsoft.EntityFrameworkCore.Migrations;

namespace SanProject.Data.Migrations
{
    public partial class Reservationsareupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hotelCity",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hotelCountry",
                table: "Reservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hotelCity",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "hotelCountry",
                table: "Reservations");
        }
    }
}
