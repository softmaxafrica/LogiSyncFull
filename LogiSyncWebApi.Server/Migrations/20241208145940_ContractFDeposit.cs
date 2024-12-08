using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSyncWebApi.Server.Migrations
{
    /// <inheritdoc />
    public partial class ContractFDeposit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_JobRequests_JobRequestID",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_JobRequestID",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "JobRequestID",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "CONTRACT_ID",
                table: "JobRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FIRST_DEPOSIT_AMOUNT",
                table: "JobRequests",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_CONTRACT_ID",
                table: "JobRequests",
                column: "CONTRACT_ID",
                unique: true,
                filter: "[CONTRACT_ID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Contracts_CONTRACT_ID",
                table: "JobRequests",
                column: "CONTRACT_ID",
                principalTable: "Contracts",
                principalColumn: "ContractID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Contracts_CONTRACT_ID",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_CONTRACT_ID",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "CONTRACT_ID",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "FIRST_DEPOSIT_AMOUNT",
                table: "JobRequests");

            migrationBuilder.AddColumn<string>(
                name: "JobRequestID",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_JobRequestID",
                table: "Contracts",
                column: "JobRequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_JobRequests_JobRequestID",
                table: "Contracts",
                column: "JobRequestID",
                principalTable: "JobRequests",
                principalColumn: "JOB_REQUEST_ID");
        }
    }
}
