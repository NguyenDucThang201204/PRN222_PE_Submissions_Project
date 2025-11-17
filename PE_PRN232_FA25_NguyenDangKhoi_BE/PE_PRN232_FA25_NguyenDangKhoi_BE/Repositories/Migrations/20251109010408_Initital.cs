using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Initital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BearAccounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BearAc__349DA58674FFF6D7", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "BearType",
                columns: table => new
                {
                    BearTypeId = table.Column<int>(type: "int", nullable: false),
                    BearTypeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Origin = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BearType__DAD4F3BE33E09654", x => x.BearTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BearProfile",
                columns: table => new
                {
                    BearProfileID = table.Column<int>(type: "int", nullable: false),
                    BearTypeId = table.Column<int>(type: "int", nullable: false),
                    BearName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    BearWeight = table.Column<int>(type: "int", unicode: false, maxLength: 50, nullable: false),
                    Characteristics = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CareNeeds = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BearProfile__785BD69FF3D8930C", x => x.BearProfileID);
                    table.ForeignKey(
                        name: "fk_BearProfile_brand",
                        column: x => x.BearTypeId,
                        principalTable: "BearType",
                        principalColumn: "BearTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BearProfile_BearTypeId",
                table: "BearProfile",
                column: "BearTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BearAccounts");

            migrationBuilder.DropTable(
                name: "BearProfile");

            migrationBuilder.DropTable(
                name: "BearType");
        }
    }
}
