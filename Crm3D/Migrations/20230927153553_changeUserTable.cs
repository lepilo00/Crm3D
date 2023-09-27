using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm3D.Migrations
{
    /// <inheritdoc />
    public partial class changeUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kategorija",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kategorija",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
