using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImotiPrediction.Migrations
{
    public partial class _2507 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RawPropertys",
                table: "RawPropertys");

            migrationBuilder.DropColumn(
                name: "RawPropertyUrl",
                table: "RawPropertys");

            migrationBuilder.DropColumn(
                name: "TotalFoors",
                table: "RawPropertys");

            migrationBuilder.RenameTable(
                name: "RawPropertys",
                newName: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Offers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "TotalFloors",
                table: "Offers",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "TotalFloors",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "RawPropertys");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "RawPropertys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "RawPropertyUrl",
                table: "RawPropertys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalFoors",
                table: "RawPropertys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RawPropertys",
                table: "RawPropertys",
                column: "Id");
        }
    }
}
