using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_PropertySearchingSite.Migrations
{
    public partial class updateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainEntracnce",
                table: "Properties",
                newName: "MainEntrance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainEntrance",
                table: "Properties",
                newName: "MainEntracnce");
        }
    }
}
