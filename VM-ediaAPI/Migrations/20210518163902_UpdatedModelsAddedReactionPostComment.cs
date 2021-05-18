using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VM_ediaAPI.Migrations
{
    public partial class UpdatedModelsAddedReactionPostComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Photos",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Photos",
                type: "text",
                nullable: true);
        }
    }
}
