using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChargableItemsMgtn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargableItems_Customers_CUSTOMER_ID",
                table: "ChargableItems");

            migrationBuilder.DropIndex(
                name: "IX_ChargableItems_CUSTOMER_ID",
                table: "ChargableItems");

            migrationBuilder.AlterColumn<string>(
                name: "CUSTOMER_ID",
                table: "ChargableItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CUSTOMER_ID",
                table: "ChargableItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChargableItems_CUSTOMER_ID",
                table: "ChargableItems",
                column: "CUSTOMER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargableItems_Customers_CUSTOMER_ID",
                table: "ChargableItems",
                column: "CUSTOMER_ID",
                principalTable: "Customers",
                principalColumn: "CUSTOMER_ID");
        }
    }
}
