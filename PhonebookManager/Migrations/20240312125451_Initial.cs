using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhonebookManager.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppClaimAppRole",
                columns: table => new
                {
                    ClaimsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClaimAppRole", x => new { x.ClaimsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AppClaimAppRole_AppClaims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "AppClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppClaimAppRole_AppRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BadgeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    ResponsibleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_AppUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_AppUsers_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChangeRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldNameId = table.Column<int>(type: "int", nullable: true),
                    NewNameId = table.Column<int>(type: "int", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequesterIdId = table.Column<int>(type: "int", nullable: true),
                    ItOperatorId = table.Column<int>(type: "int", nullable: true),
                    ImplementationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneLineId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeRequests_AppUsers_ItOperatorId",
                        column: x => x.ItOperatorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChangeRequests_AppUsers_RequesterIdId",
                        column: x => x.RequesterIdId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhoneLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    LineOwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneLines_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhoneUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Badge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneLineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneUsers_PhoneLines_PhoneLineId",
                        column: x => x.PhoneLineId,
                        principalTable: "PhoneLines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppClaimAppRole_RolesId",
                table: "AppClaimAppRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleId",
                table: "AppUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequests_ItOperatorId",
                table: "ChangeRequests",
                column: "ItOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequests_NewNameId",
                table: "ChangeRequests",
                column: "NewNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequests_OldNameId",
                table: "ChangeRequests",
                column: "OldNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequests_PhoneLineId",
                table: "ChangeRequests",
                column: "PhoneLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequests_RequesterIdId",
                table: "ChangeRequests",
                column: "RequesterIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ResponsibleId",
                table: "Departments",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneLines_DepartmentId",
                table: "PhoneLines",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneLines_LineOwnerId",
                table: "PhoneLines",
                column: "LineOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneUsers_PhoneLineId",
                table: "PhoneUsers",
                column: "PhoneLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequests_PhoneLines_PhoneLineId",
                table: "ChangeRequests",
                column: "PhoneLineId",
                principalTable: "PhoneLines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequests_PhoneUsers_NewNameId",
                table: "ChangeRequests",
                column: "NewNameId",
                principalTable: "PhoneUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequests_PhoneUsers_OldNameId",
                table: "ChangeRequests",
                column: "OldNameId",
                principalTable: "PhoneUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneLines_PhoneUsers_LineOwnerId",
                table: "PhoneLines",
                column: "LineOwnerId",
                principalTable: "PhoneUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppRoles_RoleId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AppUsers_ManagerId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AppUsers_ResponsibleId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneUsers_PhoneLines_PhoneLineId",
                table: "PhoneUsers");

            migrationBuilder.DropTable(
                name: "AppClaimAppRole");

            migrationBuilder.DropTable(
                name: "ChangeRequests");

            migrationBuilder.DropTable(
                name: "AppClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "PhoneLines");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "PhoneUsers");
        }
    }
}
