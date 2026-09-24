using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changing_the_name_of_the_Services_to_ServiceType_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderServices_Services_ServiceId",
                table: "WorkOrderServices");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "WorkOrderServices",
                newName: "ServiceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderServices_ServiceId",
                table: "WorkOrderServices",
                newName: "IX_WorkOrderServices_ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderServices_Services_ServiceTypeId",
                table: "WorkOrderServices",
                column: "ServiceTypeId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderServices_Services_ServiceTypeId",
                table: "WorkOrderServices");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeId",
                table: "WorkOrderServices",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderServices_ServiceTypeId",
                table: "WorkOrderServices",
                newName: "IX_WorkOrderServices_ServiceId");

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
