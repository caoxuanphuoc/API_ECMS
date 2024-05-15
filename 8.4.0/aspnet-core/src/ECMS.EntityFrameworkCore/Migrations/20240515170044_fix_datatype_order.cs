using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECMS.Migrations
{
    /// <inheritdoc />
    public partial class fix_datatype_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpOrderHistory_AbpOrder_OderId",
                table: "AbpOrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_AbpOrderHistory_OderId",
                table: "AbpOrderHistory");

            migrationBuilder.DropColumn(
                name: "OderId",
                table: "AbpOrderHistory");

            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
                table: "AbpOrderHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "AbpOrderHistory",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrderHistory_OrderId",
                table: "AbpOrderHistory",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpOrderHistory_AbpOrder_OrderId",
                table: "AbpOrderHistory",
                column: "OrderId",
                principalTable: "AbpOrder",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpOrderHistory_AbpOrder_OrderId",
                table: "AbpOrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_AbpOrderHistory_OrderId",
                table: "AbpOrderHistory");

            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "AbpOrderHistory");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AbpOrderHistory");

            migrationBuilder.AddColumn<long>(
                name: "OderId",
                table: "AbpOrderHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrderHistory_OderId",
                table: "AbpOrderHistory",
                column: "OderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpOrderHistory_AbpOrder_OderId",
                table: "AbpOrderHistory",
                column: "OderId",
                principalTable: "AbpOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
