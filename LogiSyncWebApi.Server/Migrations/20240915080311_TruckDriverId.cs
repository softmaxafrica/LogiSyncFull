using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class TruckDriverId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverID",
                table: "Trucks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_DriverID",
                table: "Trucks",
                column: "DriverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Drivers_DriverID",
                table: "Trucks",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "DRIVER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Drivers_DriverID",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_DriverID",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Trucks");
        }
    }
}
