using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhonebookManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppRoles_RoleId",
                table: "AppUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppRoles_RoleId",
                table: "AppUsers",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppRoles_RoleId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "AppUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AppUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppRoles_RoleId",
                table: "AppUsers",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id");
        }
    }
}
