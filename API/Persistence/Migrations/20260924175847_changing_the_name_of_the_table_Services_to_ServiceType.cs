using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changing_the_name_of_the_table_Services_to_ServiceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderServices_Services_ServiceId",
                table: "WorkOrderServices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "WorkOrderServices",
                newName: "ServiceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderServices_ServiceId",
                table: "WorkOrderServices",
                newName: "IX_WorkOrderServices_ServiceTypeId");

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceType_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_CompanyId",
                table: "ServiceType",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderServices_ServiceType_ServiceTypeId",
                table: "WorkOrderServices",
                column: "ServiceTypeId",
                principalTable: "ServiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderServices_ServiceType_ServiceTypeId",
                table: "WorkOrderServices");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeId",
                table: "WorkOrderServices",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderServices_ServiceTypeId",
                table: "WorkOrderServices",
                newName: "IX_WorkOrderServices_ServiceId");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_CompanyId",
                table: "Services",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderServices_Services_ServiceId",
                table: "WorkOrderServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
