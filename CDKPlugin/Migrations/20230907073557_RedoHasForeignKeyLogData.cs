using Microsoft.EntityFrameworkCore.Migrations;

namespace CDKPlugin.Migrations
{
    public partial class RedoHasForeignKeyLogData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogData_CDKData_CDKey",
                table: "LogData");

            migrationBuilder.AddColumn<string>(
                name: "CDKDataCKey",
                table: "LogData",
                type: "varchar(255) CHARACTER SET utf8mb4",
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
    }
}
