using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class requestU1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "STATUS",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PICKUP_LOCATION",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DELIVERY_LOCATION",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DriverID",
                table: "JobRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "REQUEST_TYPE",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TRUCK_TYPE",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_DriverID",
                table: "JobRequests",
                column: "DriverID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Drivers_DriverID",
                table: "JobRequests",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "DRIVER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Drivers_DriverID",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_DriverID",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "REQUEST_TYPE",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "TRUCK_TYPE",
                table: "JobRequests");

            migrationBuilder.AlterColumn<string>(
                name: "STATUS",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PICKUP_LOCATION",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DELIVERY_LOCATION",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
