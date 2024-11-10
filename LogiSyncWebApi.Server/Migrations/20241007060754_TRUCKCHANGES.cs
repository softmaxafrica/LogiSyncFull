using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class TRUCKCHANGES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BRAND",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CABIN_TYPE",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CATEGORY",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CHASIS_NO",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DRIVE",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ENGINE_CAPACITY",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FUEL_TYPE",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BRAND",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "CABIN_TYPE",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "CATEGORY",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "CHASIS_NO",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "DRIVE",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "ENGINE_CAPACITY",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "FUEL_TYPE",
                table: "Trucks");
        }
    }
}
