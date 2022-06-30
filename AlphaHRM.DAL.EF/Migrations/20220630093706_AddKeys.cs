using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaHRM.DAL.EF.Migrations
{
    public partial class AddKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vacation_UserID",
                table: "Vacation",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacation_User_UserID",
                table: "Vacation",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacation_User_UserID",
                table: "Vacation");

            migrationBuilder.DropIndex(
                name: "IX_Vacation_UserID",
                table: "Vacation");
        }
    }
}
