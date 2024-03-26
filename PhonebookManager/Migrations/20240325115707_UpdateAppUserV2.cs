using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhonebookManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "AppUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
