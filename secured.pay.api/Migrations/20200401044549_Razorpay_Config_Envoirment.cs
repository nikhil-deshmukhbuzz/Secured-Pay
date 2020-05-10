using Microsoft.EntityFrameworkCore.Migrations;

namespace secured.pay.api.Migrations
{
    public partial class Razorpay_Config_Envoirment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Enviorment",
                table: "Razorpay_Configs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enviorment",
                table: "Razorpay_Configs");
        }
    }
}
