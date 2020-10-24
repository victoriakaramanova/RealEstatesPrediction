using Microsoft.EntityFrameworkCore.Migrations;

namespace ImotiPrediction.Migrations
{
    public partial class SearchAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Searches",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Size = table.Column<float>(nullable: false),
                    Floor = table.Column<float>(nullable: false),
                    TotalFloors = table.Column<float>(nullable: false),
                    District = table.Column<string>(nullable: true),
                    Year = table.Column<float>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    BuildingType = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Searches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Searches_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Searches_UserId",
                table: "Searches",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Searches");
        }
    }
}
