using System.ComponentModel.DataAnnotations;

namespace gestion_stock.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "le nom de l'auruer est obligatoire")]
        [MinLength(5, ErrorMessage = "le nom doit comporter 5 cactaire")]
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Address { get; set; }
        public int Cin { get; set; }
        public int Tel { get; set; }
        public virtual ICollection<Facture> Facture { get; set; } = new List<Facture>();
    }
}
