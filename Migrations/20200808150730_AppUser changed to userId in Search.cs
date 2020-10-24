using Microsoft.EntityFrameworkCore.Migrations;

namespace ImotiPrediction.Migrations
{
    public partial class AppUserchangedtouserIdinSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Searches_AspNetUsers_UserId",
                table: "Searches");

            migrationBuilder.DropIndex(
                name: "IX_Searches_UserId",
                table: "Searches");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Searches",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Searches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Searches_UserId",
                table: "Searches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_AspNetUsers_UserId",
                table: "Searches",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
