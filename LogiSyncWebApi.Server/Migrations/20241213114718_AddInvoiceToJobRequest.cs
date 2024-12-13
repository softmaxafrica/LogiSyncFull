using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceToJobRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_JobRequests_JOB_REQUEST_ID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_JOB_REQUEST_ID",
                table: "Invoices");

            migrationBuilder.AlterColumn<string>(
                name: "JOB_REQUEST_ID",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_INVOICE_NUMBER",
                table: "JobRequests",
                column: "INVOICE_NUMBER");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Invoices_INVOICE_NUMBER",
                table: "JobRequests",
                column: "INVOICE_NUMBER",
                principalTable: "Invoices",
                principalColumn: "INVOICE_NUMBER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Invoices_INVOICE_NUMBER",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_INVOICE_NUMBER",
                table: "JobRequests");

            migrationBuilder.AlterColumn<string>(
                name: "JOB_REQUEST_ID",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_JOB_REQUEST_ID",
                table: "Invoices",
                column: "JOB_REQUEST_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_JobRequests_JOB_REQUEST_ID",
                table: "Invoices",
                column: "JOB_REQUEST_ID",
                principalTable: "JobRequests",
                principalColumn: "JOB_REQUEST_ID");
        }
    }
}
