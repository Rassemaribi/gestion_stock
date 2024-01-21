using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_stock.Migrations
{
    /// <inheritdoc />
    public partial class NomDeLaMigration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Prix",
                table: "Facture",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prix",
                table: "Facture");
        }
    }
}
