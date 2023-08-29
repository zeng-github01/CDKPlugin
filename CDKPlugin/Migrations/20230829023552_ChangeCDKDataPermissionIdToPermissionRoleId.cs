using Microsoft.EntityFrameworkCore.Migrations;

namespace CDKPlugin.Migrations
{
    public partial class ChangeCDKDataPermissionIdToPermissionRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissionID",
                table: "CDKData",
                newName: "PermissionRoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissionRoleID",
                table: "CDKData",
                newName: "PermissionID");
        }
    }
}
