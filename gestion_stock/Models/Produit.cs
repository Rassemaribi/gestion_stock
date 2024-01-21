using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_stock.Models
{
    public class Produit
    {
        public int ID { get; set; }
        public string? Nom { get; set; }
        public string? Lib { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        [NotMapped]
        public int quantiteVendue { get; set; }
        public int? Idservice { get; set; }

        public virtual Fournisseur? IdserviceNavigation { get; set; }

        public virtual ICollection<Facture> Facture { get; set; } = new List<Facture>();
    }
}
