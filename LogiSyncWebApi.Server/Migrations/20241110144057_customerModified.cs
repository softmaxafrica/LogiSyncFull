using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class customerModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PAYMENT_INFO",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "ADDRESS",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BANK_ACCOUNT_HOLDER",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BANK_ACCOUNT_NUMBER",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BANK_NAME",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BILLING_ADDRESS",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CARD_NUMBER",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CARD_TYPE",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EXPIRY_DATE",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MOBILE_NETWORK",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MOBILE_NUMBER",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PAYMENT_METHOD",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BANK_ACCOUNT_HOLDER",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BANK_ACCOUNT_NUMBER",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BANK_NAME",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BILLING_ADDRESS",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CARD_NUMBER",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CARD_TYPE",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EXPIRY_DATE",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MOBILE_NETWORK",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MOBILE_NUMBER",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PAYMENT_METHOD",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "ADDRESS",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PAYMENT_INFO",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
