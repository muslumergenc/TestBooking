using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SanProject.Data.Migrations
{
    public partial class Reservationsareadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    reservationNumber = table.Column<string>(nullable: false),
                    buyerId = table.Column<string>(nullable: true),
                    travellerNumber = table.Column<int>(nullable: false),
                    paymentNo = table.Column<int>(nullable: false),
                    paymentAmount = table.Column<double>(nullable: false),
                    paymetCurrency = table.Column<string>(nullable: true),
                    bookingNumber = table.Column<string>(nullable: true),
                    beginDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false),
                    serviceId = table.Column<string>(nullable: true),
                    hotelName = table.Column<string>(nullable: true),
                    hotelPhoneNumber = table.Column<string>(nullable: true),
                    hotelHomePage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.reservationNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
