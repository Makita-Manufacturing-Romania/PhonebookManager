using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhonebookManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppClaimV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Claim",
                table: "AppClaims",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claim",
                table: "AppClaims");
        }
    }
}
