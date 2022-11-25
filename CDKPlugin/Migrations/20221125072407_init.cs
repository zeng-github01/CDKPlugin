using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDKPlugin.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zengyj_CDKPlugin_CdkData",
                columns: table => new
                {
                    CKey = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zengyj_CDKPlugin_CdkData", x => x.CKey);
                });

            migrationBuilder.CreateTable(
                name: "Zengyj_CDKPlugin_LogData",
                columns: table => new
                {
                    CKey = table.Column<Guid>(nullable: false),
                    SteamID = table.Column<ulong>(nullable: false),
                    RedeemedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zengyj_CDKPlugin_LogData", x => x.CKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zengyj_CDKPlugin_LogData_CKey_SteamID",
                table: "Zengyj_CDKPlugin_LogData",
                columns: new[] { "CKey", "SteamID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zengyj_CDKPlugin_CdkData");

            migrationBuilder.DropTable(
                name: "Zengyj_CDKPlugin_LogData");
        }
    }
}
