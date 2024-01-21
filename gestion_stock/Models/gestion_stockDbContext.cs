using Microsoft.EntityFrameworkCore;
using gestion_stock.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace gestion_stock.Models
{
    public class gestion_stockDbContext : IdentityDbContext
    {
        internal string? Factures;

        public gestion_stockDbContext()
        {

        }

        public gestion_stockDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Facture> Facture { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
         public DbSet<Produit> Produits { get; set; }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Correction ici

            // Configure la clé primaire pour l'entité IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        }


    }


}
