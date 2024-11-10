using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class driverDetailsModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMAGE_URL",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IS_AVILABLE_FOR_BOOKING",
                table: "Drivers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LICENSE_CLASSES",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LICENSE_EXPIRE_DATE",
                table: "Drivers",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMAGE_URL",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IS_AVILABLE_FOR_BOOKING",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LICENSE_CLASSES",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LICENSE_EXPIRE_DATE",
                table: "Drivers");
        }
    }
}
