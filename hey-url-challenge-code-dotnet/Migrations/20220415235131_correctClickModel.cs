using Microsoft.EntityFrameworkCore.Migrations;

namespace hey_url_challenge_code_dotnet.Migrations
{
    public partial class correctClickModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Click_Urls_UrlId",
                table: "Click");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Click",
                table: "Click");

            migrationBuilder.RenameTable(
                name: "Click",
                newName: "Clicks");

            migrationBuilder.RenameIndex(
                name: "IX_Click_UrlId",
                table: "Clicks",
                newName: "IX_Clicks_UrlId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clicks",
                table: "Clicks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clicks_Urls_UrlId",
                table: "Clicks",
                column: "UrlId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clicks_Urls_UrlId",
                table: "Clicks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clicks",
                table: "Clicks");

            migrationBuilder.RenameTable(
                name: "Clicks",
                newName: "Click");

            migrationBuilder.RenameIndex(
                name: "IX_Clicks_UrlId",
                table: "Click",
                newName: "IX_Click_UrlId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Click",
                table: "Click",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Click_Urls_UrlId",
                table: "Click",
                column: "UrlId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
