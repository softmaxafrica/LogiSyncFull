using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class PRICEAGREEMENTUPD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "REQUESTED_PRICE",
                table: "PriceAgreements",
                newName: "COMPANY_PRICE");

            migrationBuilder.RenameColumn(
                name: "ACCEPTED_PRICE",
                table: "PriceAgreements",
                newName: "AGREED_PRICE");

            migrationBuilder.AddColumn<string>(
                name: "COMPANY_ID",
                table: "PriceAgreements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMER_ID",
                table: "PriceAgreements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "REQUEST_ID",
                table: "PriceAgreements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ASSIGNED_COMPANY",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COMPANY_ID",
                table: "PriceAgreements");

            migrationBuilder.DropColumn(
                name: "CUSTOMER_ID",
                table: "PriceAgreements");

            migrationBuilder.DropColumn(
                name: "REQUEST_ID",
                table: "PriceAgreements");

            migrationBuilder.DropColumn(
                name: "ASSIGNED_COMPANY",
                table: "JobRequests");

            migrationBuilder.RenameColumn(
                name: "COMPANY_PRICE",
                table: "PriceAgreements",
                newName: "REQUESTED_PRICE");

            migrationBuilder.RenameColumn(
                name: "AGREED_PRICE",
                table: "PriceAgreements",
                newName: "ACCEPTED_PRICE");
        }
    }
}
