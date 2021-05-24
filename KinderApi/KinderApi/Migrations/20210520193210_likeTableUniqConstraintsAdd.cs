using Microsoft.EntityFrameworkCore.Migrations;

namespace KinderApi.Migrations
{
    public partial class likeTableUniqConstraintsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Likes_SenderId",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SenderId_ReceiverId",
                table: "Likes",
                columns: new[] { "SenderId", "ReceiverId" },
                unique: true,
                filter: "[SenderId] IS NOT NULL AND [ReceiverId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Likes_SenderId_ReceiverId",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SenderId",
                table: "Likes",
                column: "SenderId");
        }
    }
}
