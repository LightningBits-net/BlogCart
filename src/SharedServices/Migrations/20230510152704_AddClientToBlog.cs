using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedServices.Migrations
{
    /// <inheritdoc />
    public partial class AddClientToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "BlogCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ClientId",
                table: "Blogs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_ClientId",
                table: "BlogCategories",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Clients_ClientId",
                table: "BlogCategories",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Clients_ClientId",
                table: "Blogs",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Clients_ClientId",
                table: "BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Clients_ClientId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ClientId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_ClientId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "BlogCategories");
        }
    }
}
