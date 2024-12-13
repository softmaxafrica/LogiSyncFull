using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddListOfTruckTypesToDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverTruckType",
                columns: table => new
                {
                    DriverID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TruckTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTruckType", x => new { x.DriverID, x.TruckTypeID });
                    table.ForeignKey(
                        name: "FK_DriverTruckType_Driver",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DRIVER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverTruckType_TruckType",
                        column: x => x.TruckTypeID,
                        principalTable: "TruckTypes",
                        principalColumn: "TRUCK_TYPE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverTruckType_TruckTypeID",
                table: "DriverTruckType",
                column: "TruckTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverTruckType");
        }
    }
}
