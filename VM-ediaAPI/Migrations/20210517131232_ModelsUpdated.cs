using Microsoft.EntityFrameworkCore.Migrations;

namespace VM_ediaAPI.Migrations
{
    public partial class ModelsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "MainPhotoUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhotoUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Photos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
