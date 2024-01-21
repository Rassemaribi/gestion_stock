using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_stock.Models
{
    [Table("Fournisseur")]
    public class Fournisseur
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du fournisseur est obligatoire")]
        [MaxLength(50, ErrorMessage = "Veuillez vérifier les champs")]
        public string? Nom { get; set; }

        [Required(ErrorMessage = "Le prénom du fournisseur est obligatoire")]
        public string? Prenom { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone du fournisseur est obligatoire")]
        public string? NumTel { get; set; }

        public string? Adresse { get; set; }

        public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
    }
}
