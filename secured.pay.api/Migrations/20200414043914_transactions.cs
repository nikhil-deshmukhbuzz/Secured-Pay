using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace secured.pay.api.Migrations
{
    public partial class transactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionSteps",
                columns: table => new
                {
                    TransactionStepID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StepID = table.Column<long>(nullable: false),
                    RazorPay_AttributeID = table.Column<long>(nullable: false),
                    PayeeName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    CustomerCode = table.Column<string>(nullable: true),
                    ApplicationUrl = table.Column<string>(nullable: true),
                    SucessUrl = table.Column<string>(nullable: true),
                    ErrorUrl = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSteps", x => x.TransactionStepID);
                    table.ForeignKey(
                        name: "FK_TransactionSteps_RazorPay_Attributes_RazorPay_AttributeID",
                        column: x => x.RazorPay_AttributeID,
                        principalTable: "RazorPay_Attributes",
                        principalColumn: "RazorPay_AttributeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionSteps_Steps_StepID",
                        column: x => x.StepID,
                        principalTable: "Steps",
                        principalColumn: "StepID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductCode = table.Column<string>(nullable: true),
                    CustomerCode = table.Column<string>(nullable: true),
                    PaymentID = table.Column<string>(nullable: true),
                    OrderID = table.Column<string>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    PayeeName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PaymentStatusID = table.Column<long>(nullable: false),
                    PaymentTypeID = table.Column<long>(nullable: false),
                    TransactionStepID = table.Column<long>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentStatuss_PaymentStatusID",
                        column: x => x.PaymentStatusID,
                        principalTable: "PaymentStatuss",
                        principalColumn: "PaymentStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentTypes_PaymentTypeID",
                        column: x => x.PaymentTypeID,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionSteps_TransactionStepID",
                        column: x => x.TransactionStepID,
                        principalTable: "TransactionSteps",
                        principalColumn: "TransactionStepID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentStatusID",
                table: "Transactions",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentTypeID",
                table: "Transactions",
                column: "PaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionStepID",
                table: "Transactions",
                column: "TransactionStepID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSteps_RazorPay_AttributeID",
                table: "TransactionSteps",
                column: "RazorPay_AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSteps_StepID",
                table: "TransactionSteps",
                column: "StepID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionSteps");
        }
    }
}
