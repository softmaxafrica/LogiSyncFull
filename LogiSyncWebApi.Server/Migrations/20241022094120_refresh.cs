using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class refresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_INVOICE_NUMBER",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_INVOICE_NUMBER",
                table: "Payments");

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
                name: "FK_Payments_Invoices_InvoiceNumber1",
                table: "Payments",
                column: "InvoiceNumber1",
                principalTable: "Invoices",
                principalColumn: "INVOICE_NUMBER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_InvoiceNumber1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InvoiceNumber1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber1",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_INVOICE_NUMBER",
                table: "Payments",
                column: "INVOICE_NUMBER");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Invoices_INVOICE_NUMBER",
                table: "Payments",
                column: "INVOICE_NUMBER",
                principalTable: "Invoices",
                principalColumn: "INVOICE_NUMBER",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
