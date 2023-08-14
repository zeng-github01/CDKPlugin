using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDKPlugin.Migrations
{
    public partial class RedoDatabaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CDKData",
                columns: table => new
                {
                    CKey = table.Column<string>(nullable: false),
                    Vehicle = table.Column<ushort>(nullable: false),
                    Reputation = table.Column<int>(nullable: false),
                    Experience = table.Column<uint>(nullable: false),
                    Money = table.Column<decimal>(nullable: false),
                    PermissionID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDKData", x => x.CKey);
                });

            migrationBuilder.CreateTable(
                name: "CDKItemWrapper",
                columns: table => new
                {
                    CDKDataCKey = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemID = table.Column<ushort>(nullable: false),
                    Amount = table.Column<byte>(nullable: false),
                    State = table.Column<byte[]>(nullable: false),
                    Quality = table.Column<byte>(nullable: false),
                    Count = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDKItemWrapper", x => new { x.CDKDataCKey, x.Id });
                    table.ForeignKey(
                        name: "FK_CDKItemWrapper_CDKData_CDKDataCKey",
                        column: x => x.CDKDataCKey,
                        principalTable: "CDKData",
                        principalColumn: "CKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogData",
                columns: table => new
                {
                    LogID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CDKey = table.Column<string>(nullable: false),
                    SteamID = table.Column<ulong>(nullable: false),
                    RedeemedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogData", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_LogData_CDKData_CDKey",
                        column: x => x.CDKey,
                        principalTable: "CDKData",
                        principalColumn: "CKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogData_CDKey_SteamID",
                table: "LogData",
                columns: new[] { "CDKey", "SteamID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CDKItemWrapper");

            migrationBuilder.DropTable(
                name: "LogData");

            migrationBuilder.DropTable(
                name: "CDKData");
        }
    }
}
