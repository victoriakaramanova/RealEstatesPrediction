using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImotiPrediction.Migrations
{
    public partial class _09082020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Searches");

            migrationBuilder.AddColumn<DateTime>(
                name: "PredictedAt",
                table: "Searches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PredictedAt",
                table: "Searches");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Searches",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
