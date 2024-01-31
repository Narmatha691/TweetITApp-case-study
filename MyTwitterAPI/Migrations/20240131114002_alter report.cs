using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTwitterAPI.Migrations
{
    public partial class alterreport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportUserId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportUserId",
                table: "Reports",
                column: "ReportUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_ReportUserId",
                table: "Reports",
                column: "ReportUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_ReportUserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReportUserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportUserId",
                table: "Reports");
        }
    }
}
