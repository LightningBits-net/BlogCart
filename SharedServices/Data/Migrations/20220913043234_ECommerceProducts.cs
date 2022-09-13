using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWeb.Server.Data.Migrations
{
    public partial class ECommerceProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECommmerceProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopFavorites = table.Column<bool>(type: "bit", nullable: false),
                    CustomerFavorites = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatgoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECommmerceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECommmerceProducts_ECommerceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ECommerceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECommmerceProducts_CategoryId",
                table: "ECommmerceProducts",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECommmerceProducts");
        }
    }
}
