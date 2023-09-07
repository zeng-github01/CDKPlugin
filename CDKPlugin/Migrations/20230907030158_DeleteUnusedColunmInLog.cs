using Microsoft.EntityFrameworkCore.Migrations;

namespace CDKPlugin.Migrations
{
    public partial class DeleteUnusedColunmInLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogData_CDKData_CDKey",
                table: "LogData");

            migrationBuilder.AddColumn<string>(
                name: "CDKDataCKey",
                table: "LogData",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogData_CDKDataCKey",
                table: "LogData",
                column: "CDKDataCKey");

            migrationBuilder.AddForeignKey(
                name: "FK_LogData_CDKData_CDKDataCKey",
                table: "LogData",
                column: "CDKDataCKey",
                principalTable: "CDKData",
                principalColumn: "CKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogData_CDKData_CDKDataCKey",
                table: "LogData");

            migrationBuilder.DropIndex(
                name: "IX_LogData_CDKDataCKey",
                table: "LogData");

            migrationBuilder.DropColumn(
                name: "CDKDataCKey",
                table: "LogData");

            migrationBuilder.AddForeignKey(
                name: "FK_LogData_CDKData_CDKey",
                table: "LogData",
                column: "CDKey",
                principalTable: "CDKData",
                principalColumn: "CKey",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
