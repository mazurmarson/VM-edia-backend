using Microsoft.EntityFrameworkCore.Migrations;

namespace VM_ediaAPI.Migrations
{
    public partial class AddedTypeOfReaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPositive",
                table: "Reactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPositive",
                table: "Reactions");
        }
    }
}
