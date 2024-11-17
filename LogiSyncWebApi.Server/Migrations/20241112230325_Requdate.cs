using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class Requdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LAST_UPDATE_TIME",
                table: "JobRequests",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LAST_UPDATE_TIME",
                table: "JobRequests");
        }
    }
}
