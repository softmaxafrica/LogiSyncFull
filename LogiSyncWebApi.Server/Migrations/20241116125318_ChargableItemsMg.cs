using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChargableItemsMg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargableItems",
                columns: table => new
                {
                    ITEM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JOB_REQUEST_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PRICE_AGREEMENT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    INVOICE_NUMBER = table.Column<int>(type: "int", nullable: true),
                    CUSTOMER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AMOUNT = table.Column<double>(type: "float", nullable: false),
                    ISSUE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargableItems", x => x.ITEM_ID);
                    table.ForeignKey(
                        name: "FK_ChargableItems_Customers_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "Customers",
                        principalColumn: "CUSTOMER_ID");
                    table.ForeignKey(
                        name: "FK_ChargableItems_JobRequests_JOB_REQUEST_ID",
                        column: x => x.JOB_REQUEST_ID,
                        principalTable: "JobRequests",
                        principalColumn: "JOB_REQUEST_ID");
                    table.ForeignKey(
                        name: "FK_ChargableItems_PriceAgreements_PRICE_AGREEMENT_ID",
                        column: x => x.PRICE_AGREEMENT_ID,
                        principalTable: "PriceAgreements",
                        principalColumn: "PRICE_AGREEMENT_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargableItems_CUSTOMER_ID",
                table: "ChargableItems",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ChargableItems_JOB_REQUEST_ID",
                table: "ChargableItems",
                column: "JOB_REQUEST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ChargableItems_PRICE_AGREEMENT_ID",
                table: "ChargableItems",
                column: "PRICE_AGREEMENT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargableItems");
        }
    }
}
