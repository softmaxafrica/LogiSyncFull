using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class Invoicing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_JobRequests_JOB_REQUEST_ID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_InvoiceNumber1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InvoiceNumber1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber1",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "AMOUNT",
                table: "Invoices",
                newName: "TOTAL_AMOUNT");

            migrationBuilder.AddColumn<double>(
                name: "OPERATIONAL_CHARGE",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PAYMENT_ID",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SERVICE_CHARGE",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_JobRequests_JOB_REQUEST_ID",
                table: "Invoices",
                column: "JOB_REQUEST_ID",
                principalTable: "JobRequests",
                principalColumn: "JOB_REQUEST_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_JobRequests_JOB_REQUEST_ID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OPERATIONAL_CHARGE",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PAYMENT_ID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SERVICE_CHARGE",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "TOTAL_AMOUNT",
                table: "Invoices",
                newName: "AMOUNT");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber1",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceNumber1",
                table: "Payments",
                column: "InvoiceNumber1");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_JobRequests_JOB_REQUEST_ID",
                table: "Invoices",
                column: "JOB_REQUEST_ID",
                principalTable: "JobRequests",
                principalColumn: "JOB_REQUEST_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Invoices_InvoiceNumber1",
                table: "Payments",
                column: "InvoiceNumber1",
                principalTable: "Invoices",
                principalColumn: "INVOICE_NUMBER");
        }
    }
}
