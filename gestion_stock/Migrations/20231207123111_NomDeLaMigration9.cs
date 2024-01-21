using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_stock.Migrations
{
    /// <inheritdoc />
    public partial class NomDeLaMigration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    IdFacture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateVente = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idproduit = table.Column<int>(type: "int", nullable: true),
                    ProduitID = table.Column<int>(type: "int", nullable: true),
                    Idclient = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    NomProduit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombrePiecesAchete = table.Column<int>(type: "int", nullable: false),
                    MontantTotalAchat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.IdFacture);
                    table.ForeignKey(
                        name: "FK_Facture_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Facture_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facture_ClientId",
                table: "Facture",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_ProduitID",
                table: "Facture",
                column: "ProduitID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facture");
        }
    }
}
