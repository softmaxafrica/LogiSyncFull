using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class ContractInvoicePayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "REFERENCE_NUMBER",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CURRENCY",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OWED_AMOUNT",
                table: "Invoices",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TOTAL_PAID_AMOUNT",
                table: "Invoices",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgreedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdvancePaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobRequestID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contracts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CUSTOMER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_JobRequests_JobRequestID",
                        column: x => x.JobRequestID,
                        principalTable: "JobRequests",
                        principalColumn: "JOB_REQUEST_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_INVOICE_NUMBER",
                table: "Payments",
                column: "INVOICE_NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CompanyID",
                table: "Contracts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerID",
                table: "Contracts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_JobRequestID",
                table: "Contracts",
                column: "JobRequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Invoices_INVOICE_NUMBER",
                table: "Payments",
                column: "INVOICE_NUMBER",
                principalTable: "Invoices",
                principalColumn: "INVOICE_NUMBER",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_INVOICE_NUMBER",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Payments_INVOICE_NUMBER",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CURRENCY",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OWED_AMOUNT",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TOTAL_PAID_AMOUNT",
                table: "Invoices");

            migrationBuilder.AlterColumn<string>(
                name: "REFERENCE_NUMBER",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
