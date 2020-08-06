using Microsoft.EntityFrameworkCore.Migrations;

namespace XTMData.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: false),
                    OriginCity = table.Column<int>(nullable: false),
                    DestinyCity = table.Column<int>(nullable: false),
                    PlaneID = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Passengers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlaneID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaneName = table.Column<string>(nullable: true),
                    PlaneType = table.Column<string>(nullable: true),
                    FuelCapacity = table.Column<int>(nullable: false),
                    KmCost = table.Column<int>(nullable: false),
                    PassengerCapacity = table.Column<int>(nullable: false),
                    MaxVelocity = table.Column<int>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    PropulsionType = table.Column<int>(nullable: false),
                    Catering = table.Column<bool>(nullable: false),
                    Wifi = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlaneID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Planes");
        }
    }
}
