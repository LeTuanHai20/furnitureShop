using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseTools.Migrations
{
    public partial class InitFirstDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_AT = table.Column<DateTime>(nullable: true),
                    LAST_UPDATE_AT = table.Column<DateTime>(nullable: true),
                    CREATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    LAST_UPDATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    ROLE_CODE = table.Column<string>(maxLength: 50, nullable: true),
                    ROLE_NAME = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_AT = table.Column<DateTime>(nullable: true),
                    LAST_UPDATE_AT = table.Column<DateTime>(nullable: true),
                    CREATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    LAST_UPDATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    FULL_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    USER_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    PASSWORD = table.Column<string>(maxLength: 255, nullable: true),
                    EMAIL = table.Column<string>(maxLength: 255, nullable: true),
                    PHONE_NO = table.Column<string>(maxLength: 20, nullable: true),
                    DAY_OF_BIRTH = table.Column<DateTime>(nullable: true),
                    GENDER = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLE",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    ROLE_ID = table.Column<string>(maxLength: 50, nullable: true),
                    USER_ID = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_ROLES_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_ROLE_ID",
                table: "USER_ROLE",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_USER_ID",
                table: "USER_ROLE",
                column: "USER_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER_ROLE");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
