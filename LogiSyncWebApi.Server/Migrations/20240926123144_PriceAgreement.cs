using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class PriceAgreement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceAgreements_JobRequests_JOB_REQUEST_ID",
                table: "PriceAgreements");

            migrationBuilder.DropIndex(
                name: "IX_PriceAgreements_JOB_REQUEST_ID",
                table: "PriceAgreements");

            migrationBuilder.DropColumn(
                name: "JOB_REQUEST_ID",
                table: "PriceAgreements");

            migrationBuilder.AlterColumn<double>(
                name: "REQUESTED_PRICE",
                table: "PriceAgreements",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "ACCEPTED_PRICE",
                table: "PriceAgreements",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "CUSTOMER_PRICE",
                table: "PriceAgreements",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CUSTOMER_PRICE",
                table: "PriceAgreements");

            migrationBuilder.AlterColumn<double>(
                name: "REQUESTED_PRICE",
                table: "PriceAgreements",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ACCEPTED_PRICE",
                table: "PriceAgreements",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JOB_REQUEST_ID",
                table: "PriceAgreements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PriceAgreements_JOB_REQUEST_ID",
                table: "PriceAgreements",
                column: "JOB_REQUEST_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceAgreements_JobRequests_JOB_REQUEST_ID",
                table: "PriceAgreements",
                column: "JOB_REQUEST_ID",
                principalTable: "JobRequests",
                principalColumn: "JOB_REQUEST_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
