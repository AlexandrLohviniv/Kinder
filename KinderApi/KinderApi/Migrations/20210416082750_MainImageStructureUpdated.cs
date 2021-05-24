using Microsoft.EntityFrameworkCore.Migrations;

namespace KinderApi.Migrations
{
    public partial class MainImageStructureUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Image",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "MainImageUrl",
                table: "Users",
                type: "int",
                nullable: true);
        }
    }
}
