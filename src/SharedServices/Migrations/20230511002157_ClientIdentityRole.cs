using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedServices.Migrations
{
    public partial class ClientIdentityRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES ('CLIf8baf-26b5-4bdc-982d-6f30017f5c4c', 'Client', 'CLIENT', '')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles] WHERE [Id] = 'CLIf8baf-26b5-4bdc-982d-6f30017f5c4c'");
        }
    }
}
