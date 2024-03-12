using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECMS.Migrations
{
    /// <inheritdoc />
    public partial class add_ScheduleClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpSchedule_AbpClass_ClassId",
                table: "AbpSchedule");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "AbpSchedule",
                newName: "ScheduleClassId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpSchedule_ClassId",
                table: "AbpSchedule",
                newName: "IX_AbpSchedule_ScheduleClassId");

            migrationBuilder.CreateTable(
                name: "ScheduleClasses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleClasses_AbpClass_ClassId",
                        column: x => x.ClassId,
                        principalTable: "AbpClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleClasses_ClassId",
                table: "ScheduleClasses",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpSchedule_ScheduleClasses_ScheduleClassId",
                table: "AbpSchedule",
                column: "ScheduleClassId",
                principalTable: "ScheduleClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpSchedule_ScheduleClasses_ScheduleClassId",
                table: "AbpSchedule");

            migrationBuilder.DropTable(
                name: "ScheduleClasses");

            migrationBuilder.RenameColumn(
                name: "ScheduleClassId",
                table: "AbpSchedule",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpSchedule_ScheduleClassId",
                table: "AbpSchedule",
                newName: "IX_AbpSchedule_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpSchedule_AbpClass_ClassId",
                table: "AbpSchedule",
                column: "ClassId",
                principalTable: "AbpClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
