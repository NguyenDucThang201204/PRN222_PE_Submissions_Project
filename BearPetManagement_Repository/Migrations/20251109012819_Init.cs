using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BearPetManagement_Repository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BearAccounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "BearTypes",
                columns: table => new
                {
                    BearTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BearTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BearType", x => x.BearTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BearProfiles",
                columns: table => new
                {
                    BearProfileId = table.Column<int>(type: "int", nullable: false),
                    BearTypeId = table.Column<int>(type: "int", nullable: false),
                    BearName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BearWeight = table.Column<int>(type: "int", nullable: false),
                    Characteristics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CareNeeds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BearProfile", x => x.BearProfileId);
                    table.ForeignKey(
                        name: "FK_BearProfile_BearType",
                        column: x => x.BearProfileId,
                        principalTable: "BearTypes",
                        principalColumn: "BearTypeId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BearAccounts");

            migrationBuilder.DropTable(
                name: "BearProfiles");

            migrationBuilder.DropTable(
                name: "BearTypes");
        }
    }
}
