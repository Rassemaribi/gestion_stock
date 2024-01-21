using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_stock.Models
{
    public class Facture
    {
        [Key]
        public int IdFacture { get; set; }
        public DateTime DateVente { get; set; }

        public int? Idproduit { get; set; }
        public virtual Produit? Produit { get; set; } // Relation avec Produit

        public int? Idclient { get; set; }
        public virtual Client? Client { get; set; } // Relation avec Client

        public string? NomProduit { get; set; }
        public string? NomClient { get; set; }
        public int Prix {  get; set; }  
        public int NombrePiecesAchete { get; set; }
 
        public double MontantTotalAchat { get; set; }
    }

}
