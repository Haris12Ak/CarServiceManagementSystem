using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class added_new_tables_InspectionItemDefinition_InspectionCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "InspectionItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "InspectionItems");

            migrationBuilder.AddColumn<int>(
                name: "InspectionItemDefinitionId",
                table: "InspectionItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InspectionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionCategories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionItemDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    InspectionCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionItemDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionItemDefinition_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectionItemDefinition_InspectionCategories_InspectionCategoryId",
                        column: x => x.InspectionCategoryId,
                        principalTable: "InspectionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionItems_InspectionItemDefinitionId",
                table: "InspectionItems",
                column: "InspectionItemDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionCategories_CompanyId",
                table: "InspectionCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionItemDefinition_CompanyId",
                table: "InspectionItemDefinition",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionItemDefinition_InspectionCategoryId",
                table: "InspectionItemDefinition",
                column: "InspectionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionItems_InspectionItemDefinition_InspectionItemDefinitionId",
                table: "InspectionItems",
                column: "InspectionItemDefinitionId",
                principalTable: "InspectionItemDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionItems_InspectionItemDefinition_InspectionItemDefinitionId",
                table: "InspectionItems");

            migrationBuilder.DropTable(
                name: "InspectionItemDefinition");

            migrationBuilder.DropTable(
                name: "InspectionCategories");

            migrationBuilder.DropIndex(
                name: "IX_InspectionItems_InspectionItemDefinitionId",
                table: "InspectionItems");

            migrationBuilder.DropColumn(
                name: "InspectionItemDefinitionId",
                table: "InspectionItems");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "InspectionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InspectionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
