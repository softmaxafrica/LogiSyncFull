using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class logisync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    COMPANY_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COMPANY_TIN_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COMPANY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADMIN_FULL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADMIN_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADMIN_CONTACT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COMPANY_ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COMPANY_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COMPANY_LATITUDE = table.Column<double>(type: "float", nullable: true),
                    COMPANY_LONGITUDE = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.COMPANY_ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CUSTOMER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FULL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAYMENT_INFO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CUSTOMER_ID);
                });

            migrationBuilder.CreateTable(
                name: "GenTableColumns",
                columns: table => new
                {
                    CODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TABLE_CODE = table.Column<long>(type: "bigint", nullable: true),
                    FIELD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEADER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WIDTH = table.Column<long>(type: "bigint", nullable: true),
                    TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COLUMN_DISPLAY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COLUMN_INCLUDED = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COLUMN_INDEX = table.Column<long>(type: "bigint", nullable: true),
                    LAST_ACTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CUSER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EUSER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EDATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenTableColumns", x => x.CODE);
                });

            migrationBuilder.CreateTable(
                name: "GenTables",
                columns: table => new
                {
                    CODE = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAST_ACTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CUSER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EUSER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EDATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenTables", x => x.CODE);
                });

            migrationBuilder.CreateTable(
                name: "GenTranslations",
                columns: table => new
                {
                    CODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LANGUAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAST_ACTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CUSER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EUSER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EDATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenTranslations", x => x.CODE);
                });

            migrationBuilder.CreateTable(
                name: "SecUsers",
                columns: table => new
                {
                    USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PASSWORD_HASH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LAST_LOGIN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecUsers", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "TruckTypes",
                columns: table => new
                {
                    TRUCK_TYPE_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TYPE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckTypes", x => x.TRUCK_TYPE_ID);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DRIVER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FULL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LICENSE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REGISTRATION_COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DRIVER_ID);
                    table.ForeignKey(
                        name: "FK_Drivers_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    TRUCK_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TRUCK_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MODEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COMPANY_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TRUCK_TYPE_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.TRUCK_ID);
                    table.ForeignKey(
                        name: "FK_Trucks_Companies_COMPANY_ID",
                        column: x => x.COMPANY_ID,
                        principalTable: "Companies",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trucks_TruckTypes_TRUCK_TYPE_ID",
                        column: x => x.TRUCK_TYPE_ID,
                        principalTable: "TruckTypes",
                        principalColumn: "TRUCK_TYPE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LOCATION_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TRUCK_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LONGITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIME_STAMP = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LOCATION_ID);
                    table.ForeignKey(
                        name: "FK_Locations_Trucks_TRUCK_ID",
                        column: x => x.TRUCK_ID,
                        principalTable: "Trucks",
                        principalColumn: "TRUCK_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    INVOICE_NUMBER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSTOMER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JOB_REQUEST_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AMOUNT = table.Column<double>(type: "float", nullable: false),
                    ISSUE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DUE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.INVOICE_NUMBER);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "Customers",
                        principalColumn: "CUSTOMER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PAYMENT_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    INVOICE_NUMBER = table.Column<int>(type: "int", nullable: false),
                    AMOUNT_PAID = table.Column<double>(type: "float", nullable: false),
                    PAYMENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAYMENT_METHOD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    REFERENCE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PAYMENT_ID);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_INVOICE_NUMBER",
                        column: x => x.INVOICE_NUMBER,
                        principalTable: "Invoices",
                        principalColumn: "INVOICE_NUMBER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRequests",
                columns: table => new
                {
                    JOB_REQUEST_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PICKUP_LOCATION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DELIVERY_LOCATION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARGO_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CONTAINER_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRICE_AGREEMENT_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TruckID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequests", x => x.JOB_REQUEST_ID);
                    table.ForeignKey(
                        name: "FK_JobRequests_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CUSTOMER_ID");
                    table.ForeignKey(
                        name: "FK_JobRequests_Trucks_TruckID",
                        column: x => x.TruckID,
                        principalTable: "Trucks",
                        principalColumn: "TRUCK_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceAgreements",
                columns: table => new
                {
                    PRICE_AGREEMENT_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    REQUESTED_PRICE = table.Column<double>(type: "float", nullable: false),
                    ACCEPTED_PRICE = table.Column<double>(type: "float", nullable: false),
                    JOB_REQUEST_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceAgreements", x => x.PRICE_AGREEMENT_ID);
                    table.ForeignKey(
                        name: "FK_PriceAgreements_JobRequests_JOB_REQUEST_ID",
                        column: x => x.JOB_REQUEST_ID,
                        principalTable: "JobRequests",
                        principalColumn: "JOB_REQUEST_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CompanyID",
                table: "Drivers",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CUSTOMER_ID",
                table: "Invoices",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_JOB_REQUEST_ID",
                table: "Invoices",
                column: "JOB_REQUEST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_CustomerID",
                table: "JobRequests",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_PRICE_AGREEMENT_ID",
                table: "JobRequests",
                column: "PRICE_AGREEMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_TruckID",
                table: "JobRequests",
                column: "TruckID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TRUCK_ID",
                table: "Locations",
                column: "TRUCK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_INVOICE_NUMBER",
                table: "Payments",
                column: "INVOICE_NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_PriceAgreements_JOB_REQUEST_ID",
                table: "PriceAgreements",
                column: "JOB_REQUEST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_COMPANY_ID",
                table: "Trucks",
                column: "COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_TRUCK_TYPE_ID",
                table: "Trucks",
                column: "TRUCK_TYPE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_JobRequests_JOB_REQUEST_ID",
                table: "Invoices",
                column: "JOB_REQUEST_ID",
                principalTable: "JobRequests",
                principalColumn: "JOB_REQUEST_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_PriceAgreements_PRICE_AGREEMENT_ID",
                table: "JobRequests",
                column: "PRICE_AGREEMENT_ID",
                principalTable: "PriceAgreements",
                principalColumn: "PRICE_AGREEMENT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Companies_COMPANY_ID",
                table: "Trucks");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Customers_CustomerID",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceAgreements_JobRequests_JOB_REQUEST_ID",
                table: "PriceAgreements");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "GenTableColumns");

            migrationBuilder.DropTable(
                name: "GenTables");

            migrationBuilder.DropTable(
                name: "GenTranslations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "SecUsers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "JobRequests");

            migrationBuilder.DropTable(
                name: "PriceAgreements");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "TruckTypes");
        }
    }
}
