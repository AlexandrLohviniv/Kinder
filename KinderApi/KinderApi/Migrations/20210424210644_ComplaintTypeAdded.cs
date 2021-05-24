using Microsoft.EntityFrameworkCore.Migrations;

namespace KinderApi.Migrations
{
    public partial class ComplaintTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "complaint",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "complaint",
                table: "Complaints");
        }
    }
}
