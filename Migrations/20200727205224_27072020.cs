using Microsoft.EntityFrameworkCore.Migrations;

namespace ImotiPrediction.Migrations
{
    public partial class _27072020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pic",
                table: "Offers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pic",
                table: "Offers");
        }
    }
}
