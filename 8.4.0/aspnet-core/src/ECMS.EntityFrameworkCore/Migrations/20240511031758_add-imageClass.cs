using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECMS.Migrations
{
    /// <inheritdoc />
    public partial class addimageClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AbpClass",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AbpClass");
        }
    }
}
