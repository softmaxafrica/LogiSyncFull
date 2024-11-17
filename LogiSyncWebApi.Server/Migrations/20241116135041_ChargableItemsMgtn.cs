using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChargableItemsMgtn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargableItems_PriceAgreements_PRICE_AGREEMENT_ID",
                table: "ChargableItems");

            migrationBuilder.DropIndex(
                name: "IX_ChargableItems_PRICE_AGREEMENT_ID",
                table: "ChargableItems");

            migrationBuilder.AlterColumn<string>(
                name: "PRICE_AGREEMENT_ID",
                table: "ChargableItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ITEM_DESCRRIPTION",
                table: "ChargableItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ITEM_DESCRRIPTION",
                table: "ChargableItems");

            migrationBuilder.AlterColumn<string>(
                name: "PRICE_AGREEMENT_ID",
                table: "ChargableItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChargableItems_PRICE_AGREEMENT_ID",
                table: "ChargableItems",
                column: "PRICE_AGREEMENT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargableItems_PriceAgreements_PRICE_AGREEMENT_ID",
                table: "ChargableItems",
                column: "PRICE_AGREEMENT_ID",
                principalTable: "PriceAgreements",
                principalColumn: "PRICE_AGREEMENT_ID");
        }
    }
}
