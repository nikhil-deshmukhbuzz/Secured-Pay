using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace secured.pay.api.Migrations
{
    public partial class pay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentStatuss",
                columns: table => new
                {
                    PaymentStatusID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatuss", x => x.PaymentStatusID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypeID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "RazorPay_Attributes",
                columns: table => new
                {
                    RazorPay_AttributeID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id = table.Column<string>(nullable: true),
                    entity = table.Column<string>(nullable: true),
                    amount = table.Column<string>(nullable: true),
                    amount_paid = table.Column<string>(nullable: true),
                    amount_due = table.Column<string>(nullable: true),
                    currency = table.Column<string>(nullable: true),
                    receipt = table.Column<string>(nullable: true),
                    offer_id = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    attempts = table.Column<string>(nullable: true),
                    created_at = table.Column<string>(nullable: true),
                    checkout_config_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RazorPay_Attributes", x => x.RazorPay_AttributeID);
                });

            migrationBuilder.CreateTable(
                name: "Razorpay_Configs",
                columns: table => new
                {
                    Razorpay_ConfigID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Enviorment = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razorpay_Configs", x => x.Razorpay_ConfigID);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    StepID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.StepID);
                });

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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    SuscriptionNumber = table.Column<string>(nullable: true),
                    InvoiceNumber = table.Column<string>(nullable: true)
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
                name: "C_Transactions",
                columns: table => new
                {
                    C_TransactionID = table.Column<long>(nullable: false)
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
                    table.PrimaryKey("PK_C_Transactions", x => x.C_TransactionID);
                    table.ForeignKey(
                        name: "FK_C_Transactions_PaymentStatuss_PaymentStatusID",
                        column: x => x.PaymentStatusID,
                        principalTable: "PaymentStatuss",
                        principalColumn: "PaymentStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_C_Transactions_PaymentTypes_PaymentTypeID",
                        column: x => x.PaymentTypeID,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_C_Transactions_TransactionSteps_TransactionStepID",
                        column: x => x.TransactionStepID,
                        principalTable: "TransactionSteps",
                        principalColumn: "TransactionStepID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_C_Transactions_PaymentStatusID",
                table: "C_Transactions",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_C_Transactions_PaymentTypeID",
                table: "C_Transactions",
                column: "PaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_C_Transactions_TransactionStepID",
                table: "C_Transactions",
                column: "TransactionStepID");

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
                name: "C_Transactions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Razorpay_Configs");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "PaymentStatuss");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "TransactionSteps");

            migrationBuilder.DropTable(
                name: "RazorPay_Attributes");

            migrationBuilder.DropTable(
                name: "Steps");
        }
    }
}
