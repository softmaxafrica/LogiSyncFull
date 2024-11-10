using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class CHANGETRUCKMODEL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TRUCK_STATE",
                table: "Trucks");

            migrationBuilder.AddColumn<bool>(
                name: "IS_ACTIVE",
                table: "Trucks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IS_ACTIVE",
                table: "Trucks");

            migrationBuilder.AddColumn<string>(
                name: "TRUCK_STATE",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
