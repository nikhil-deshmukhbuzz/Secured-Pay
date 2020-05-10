using Microsoft.EntityFrameworkCore.Migrations;

namespace secured.pay.api.Migrations
{
    public partial class suscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SuscriptionNumber",
                table: "TransactionSteps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuscriptionNumber",
                table: "TransactionSteps");
        }
    }
}
