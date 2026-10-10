using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class added_tables_CompanyWorkingHours_CompanyWorkingHourExceptions_AppointmentServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentDuration",
                table: "CompanySettings",
                newName: "MinimumBookingNoticeHours");

            migrationBuilder.AddColumn<bool>(
                name: "AllowSameDayBooking",
                table: "CompanySettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BookingIntervalMinutes",
                table: "CompanySettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumBookingDaysAhead",
                table: "CompanySettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "OnlineBookingEnabled",
                table: "CompanySettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireAppointmentConfirmation",
                table: "CompanySettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppointmentServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CompanyWorkingHourExceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    OpenAt = table.Column<TimeOnly>(type: "time", nullable: true),
                    CloseAt = table.Column<TimeOnly>(type: "time", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorkingHourExceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyWorkingHourExceptions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyWorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpenAt = table.Column<TimeOnly>(type: "time", nullable: false),
                    CloseAt = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyWorkingHours_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_AppointmentId_ServiceTypeId",
                table: "AppointmentServices",
                columns: new[] { "AppointmentId", "ServiceTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_ServiceTypeId",
                table: "AppointmentServices",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkingHourExceptions_CompanyId",
                table: "CompanyWorkingHourExceptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkingHours_CompanyId",
                table: "CompanyWorkingHours",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentServices");

            migrationBuilder.DropTable(
                name: "CompanyWorkingHourExceptions");

            migrationBuilder.DropTable(
                name: "CompanyWorkingHours");

            migrationBuilder.DropColumn(
                name: "AllowSameDayBooking",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "BookingIntervalMinutes",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "MaximumBookingDaysAhead",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "OnlineBookingEnabled",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "RequireAppointmentConfirmation",
                table: "CompanySettings");

            migrationBuilder.RenameColumn(
                name: "MinimumBookingNoticeHours",
                table: "CompanySettings",
                newName: "AppointmentDuration");
        }
    }
}
