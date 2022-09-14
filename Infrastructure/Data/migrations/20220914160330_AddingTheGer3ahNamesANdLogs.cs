using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.migrations
{
    public partial class AddingTheGer3ahNamesANdLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CeatedDat",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.CreateTable(
                name: "Ger3ahLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PickerName = table.Column<string>(type: "TEXT", nullable: true),
                    PickedName = table.Column<string>(type: "TEXT", nullable: true),
                    CraetedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ger3ahLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ger3ahNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userIdId = table.Column<int>(type: "INTEGER", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    IsTaken = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateOfPicking = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ger3ahNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ger3ahNames_Users_userIdId",
                        column: x => x.userIdId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ger3ahNames_userIdId",
                table: "Ger3ahNames",
                column: "userIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ger3ahLogs");

            migrationBuilder.DropTable(
                name: "Ger3ahNames");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "CeatedDat");
        }
    }
}
