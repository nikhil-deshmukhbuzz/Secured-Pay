using Microsoft.EntityFrameworkCore.Migrations;

namespace secured.pay.api.Migrations
{
    public partial class Razorpay_Config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Razorpay_Configs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Razorpay_Configs");
        }
    }
}
