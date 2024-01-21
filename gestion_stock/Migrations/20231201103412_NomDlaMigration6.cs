using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_stock.Migrations
{
    /// <inheritdoc />
    public partial class NomDlaMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idservice",
                table: "Produits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdserviceNavigationId",
                table: "Produits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produits_IdserviceNavigationId",
                table: "Produits",
                column: "IdserviceNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Fournisseur_IdserviceNavigationId",
                table: "Produits",
                column: "IdserviceNavigationId",
                principalTable: "Fournisseur",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Fournisseur_IdserviceNavigationId",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_IdserviceNavigationId",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "Idservice",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "IdserviceNavigationId",
                table: "Produits");
        }
    }
}
