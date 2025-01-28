using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class PriceNegotiationList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "REQUEST_ID",
                table: "PriceAgreements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceAgreements_REQUEST_ID",
                table: "PriceAgreements",
                column: "REQUEST_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceAgreements_JobRequests_REQUEST_ID",
                table: "PriceAgreements",
                column: "REQUEST_ID",
                principalTable: "JobRequests",
                principalColumn: "JOB_REQUEST_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceAgreements_JobRequests_REQUEST_ID",
                table: "PriceAgreements");

            migrationBuilder.DropIndex(
                name: "IX_PriceAgreements_REQUEST_ID",
                table: "PriceAgreements");

            migrationBuilder.AlterColumn<string>(
                name: "REQUEST_ID",
                table: "PriceAgreements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
