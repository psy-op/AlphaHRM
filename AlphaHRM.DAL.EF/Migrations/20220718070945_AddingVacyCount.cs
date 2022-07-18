using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaHRM.DAL.EF.Migrations
{
    public partial class AddingVacyCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VacationCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VacationCount",
                table: "User");
        }
    }
}
