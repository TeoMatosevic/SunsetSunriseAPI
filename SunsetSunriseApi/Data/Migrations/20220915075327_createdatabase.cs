using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunsetSunriseApi.Data.Migrations
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentTimeZones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    zoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zoneDif = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentTimeZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SunsetSunrises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sunrise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sunset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    solar_noon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    day_length = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    civil_twilight_begin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    civil_twilight_end = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nautical_twilight_begin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nautical_twilight_end = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    astronomical_twilight_begin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    astronomical_twilight_end = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SunsetSunrises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeAdjustments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adjusstment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeAdjustments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeZones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    zoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zoneDif = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZones", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentTimeZones");

            migrationBuilder.DropTable(
                name: "SunsetSunrises");

            migrationBuilder.DropTable(
                name: "TimeAdjustments");

            migrationBuilder.DropTable(
                name: "TimeZones");
        }
    }
}
