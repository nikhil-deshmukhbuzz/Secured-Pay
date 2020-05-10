using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace secured.pay.api.Migrations
{
    public partial class Razorpay_Config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Razorpay_Configs",
                columns: table => new
                {
                    Razorpay_ConfigID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razorpay_Configs", x => x.Razorpay_ConfigID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSteps_OrganisationID",
                table: "TransactionSteps",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSteps_RazorPay_AttributeID",
                table: "TransactionSteps",
                column: "RazorPay_AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSteps_StepID",
                table: "TransactionSteps",
                column: "StepID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSteps_Organisations_OrganisationID",
                table: "TransactionSteps",
                column: "OrganisationID",
                principalTable: "Organisations",
                principalColumn: "OrganisationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSteps_RazorPay_Attributes_RazorPay_AttributeID",
                table: "TransactionSteps",
                column: "RazorPay_AttributeID",
                principalTable: "RazorPay_Attributes",
                principalColumn: "RazorPay_AttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSteps_Steps_StepID",
                table: "TransactionSteps",
                column: "StepID",
                principalTable: "Steps",
                principalColumn: "StepID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSteps_Organisations_OrganisationID",
                table: "TransactionSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSteps_RazorPay_Attributes_RazorPay_AttributeID",
                table: "TransactionSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSteps_Steps_StepID",
                table: "TransactionSteps");

            migrationBuilder.DropTable(
                name: "Razorpay_Configs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionSteps_OrganisationID",
                table: "TransactionSteps");

            migrationBuilder.DropIndex(
                name: "IX_TransactionSteps_RazorPay_AttributeID",
                table: "TransactionSteps");

            migrationBuilder.DropIndex(
                name: "IX_TransactionSteps_StepID",
                table: "TransactionSteps");
        }
    }
}
