using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedServices.Migrations
{
    /// <inheritdoc />
    public partial class ConversationContextPropUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Context",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Context",
                table: "Conversations");
        }
    }
}
