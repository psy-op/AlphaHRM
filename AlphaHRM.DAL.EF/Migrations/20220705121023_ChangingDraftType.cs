using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaHRM.DAL.EF.Migrations
{
    public partial class ChangingDraftType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsDraft",
                table: "Vacation",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDraft",
                table: "Vacation",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
