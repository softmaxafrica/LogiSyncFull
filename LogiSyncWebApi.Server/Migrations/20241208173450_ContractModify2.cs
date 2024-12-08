using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class ContractModify2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Companies_CompanyID",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_CustomerID",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Contracts",
                newName: "STATUS");

            migrationBuilder.RenameColumn(
                name: "TermsAndConditions",
                table: "Contracts",
                newName: "TERMS_AND_CONDITIONS");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "Contracts",
                newName: "REQUEST_ID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Contracts",
                newName: "CUSTOMER_ID");

            migrationBuilder.RenameColumn(
                name: "ContractDate",
                table: "Contracts",
                newName: "CONTRACT_DATE");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Contracts",
                newName: "COMPANY_ID");

            migrationBuilder.RenameColumn(
                name: "AgreedPrice",
                table: "Contracts",
                newName: "AGREED_PRICE");

            migrationBuilder.RenameColumn(
                name: "AdvancePaymentDate",
                table: "Contracts",
                newName: "ADVANCE_PAYMENT_DATE");

            migrationBuilder.RenameColumn(
                name: "AdvancePayment",
                table: "Contracts",
                newName: "ADVANCE_PAYMENT");

            migrationBuilder.RenameColumn(
                name: "ContractID",
                table: "Contracts",
                newName: "CONTRACT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerID",
                table: "Contracts",
                newName: "IX_Contracts_CUSTOMER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CompanyID",
                table: "Contracts",
                newName: "IX_Contracts_COMPANY_ID");

            migrationBuilder.AlterColumn<string>(
                name: "STATUS",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TERMS_AND_CONDITIONS",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "REQUEST_ID",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CUSTOMER_ID",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CONTRACT_DATE",
                table: "Contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "COMPANY_ID",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Companies_COMPANY_ID",
                table: "Contracts",
                column: "COMPANY_ID",
                principalTable: "Companies",
                principalColumn: "COMPANY_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CUSTOMER_ID",
                table: "Contracts",
                column: "CUSTOMER_ID",
                principalTable: "Customers",
                principalColumn: "CUSTOMER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Companies_COMPANY_ID",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_CUSTOMER_ID",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "STATUS",
                table: "Contracts",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "TERMS_AND_CONDITIONS",
                table: "Contracts",
                newName: "TermsAndConditions");

            migrationBuilder.RenameColumn(
                name: "REQUEST_ID",
                table: "Contracts",
                newName: "RequestID");

            migrationBuilder.RenameColumn(
                name: "CUSTOMER_ID",
                table: "Contracts",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "CONTRACT_DATE",
                table: "Contracts",
                newName: "ContractDate");

            migrationBuilder.RenameColumn(
                name: "COMPANY_ID",
                table: "Contracts",
                newName: "CompanyID");

            migrationBuilder.RenameColumn(
                name: "AGREED_PRICE",
                table: "Contracts",
                newName: "AgreedPrice");

            migrationBuilder.RenameColumn(
                name: "ADVANCE_PAYMENT_DATE",
                table: "Contracts",
                newName: "AdvancePaymentDate");

            migrationBuilder.RenameColumn(
                name: "ADVANCE_PAYMENT",
                table: "Contracts",
                newName: "AdvancePayment");

            migrationBuilder.RenameColumn(
                name: "CONTRACT_ID",
                table: "Contracts",
                newName: "ContractID");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CUSTOMER_ID",
                table: "Contracts",
                newName: "IX_Contracts_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_COMPANY_ID",
                table: "Contracts",
                newName: "IX_Contracts_CompanyID");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TermsAndConditions",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestID",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractDate",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyID",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Companies_CompanyID",
                table: "Contracts",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "COMPANY_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CustomerID",
                table: "Contracts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CUSTOMER_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
