using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdateReqForeignKeyOnPAgreement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_PriceAgreements_PRICE_AGREEMENT_ID",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_PRICE_AGREEMENT_ID",
                table: "JobRequests");

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "JobRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_PRICE_AGREEMENT_ID",
                table: "JobRequests",
                column: "PRICE_AGREEMENT_ID",
                unique: true,
                filter: "[PRICE_AGREEMENT_ID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_PriceAgreements_PRICE_AGREEMENT_ID",
                table: "JobRequests",
                column: "PRICE_AGREEMENT_ID",
                principalTable: "PriceAgreements",
                principalColumn: "PRICE_AGREEMENT_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_PriceAgreements_PRICE_AGREEMENT_ID",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_PRICE_AGREEMENT_ID",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "JobRequests");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_PRICE_AGREEMENT_ID",
                table: "JobRequests",
                column: "PRICE_AGREEMENT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_PriceAgreements_PRICE_AGREEMENT_ID",
                table: "JobRequests",
                column: "PRICE_AGREEMENT_ID",
                principalTable: "PriceAgreements",
                principalColumn: "PRICE_AGREEMENT_ID");
        }
    }
}
