using gestion_stock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace gestion_stock.Controllers
{
    public class ProduitController : Controller
    {
        private readonly gestion_stockDbContext _context;

        public ProduitController(gestion_stockDbContext context)
        {
            _context = context;
        }

        // GET: ProduitController
        [Authorize]
        // Modifier la signature de l'action Index pour accepter une chaîne de recherche
        public IActionResult Index(string searchNom)
        {
            // Filtrer les produits en fonction du nom de recherche
            var produits = _context.Produits
                .Include(x => x.IdserviceNavigation)
                .Where(p => string.IsNullOrEmpty(searchNom) || p.Nom.Contains(searchNom))
                .ToList();

            // Passer les produits filtrés à la vue
            return View(produits);
        }

        // GET: ProduitController/Details/5
        [Authorize]
        public IActionResult Details(int id)
        {
            var produit = _context.Produits.FirstOrDefault(p => p.ID == id);
            return View(produit);
        }

        // GET: ProduitController/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.FournisseurList = _context.Fournisseurs.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produit produit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_context.Produits.Where(x => x.Nom == produit.Nom).Count() > 0)
                    {
                        ViewBag.FournisseurList = _context.Fournisseurs.ToList();
                        ViewBag.errorMessage = " existe";
                        return View(produit);
                    }
                    _context.Produits.Add(produit);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.FournisseurList = _context.Fournisseurs.ToList();
                return View(produit);
            }
            catch
            {
                return View();
            }
        }


        // GET: ProduitController/Delete/5
        [Authorize]
        public IActionResult Delete(int id)
        {
            var produit = _context.Produits.FirstOrDefault(p => p.ID == id);
            return View(produit);
        }

        // POST: ProduitController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var produit = _context.Produits.FirstOrDefault(p => p.ID == id);
            _context.Produits.Remove(produit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: ProduitController/Edit/5
        public IActionResult Edit(int id)
        {
            var produit = _context.Produits.FirstOrDefault(p => p.ID == id);

            if (produit == null)
            {
                return NotFound();
            }

            ViewBag.FournisseurList = _context.Fournisseurs.ToList();
            return View(produit);
        }

        // POST: ProduitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Produit produit)
        {
            if (id != produit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produit);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FournisseurList = _context.Fournisseurs.ToList();
            return View(produit);
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.ID == id);
        }
        [Authorize]
        public IActionResult Vente(int id)
        {
            // Logique pour la vente de l'article avec l'ID donné
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vente(int id, int quantiteVendue)
        {
            var produit = await _context.Produits.FindAsync(id);

            if (produit == null)
            {
                return NotFound();
            }

            if (produit.Quantite >= quantiteVendue)
            {
                double montant = (double)(quantiteVendue * produit.Prix);
                produit.Quantite -= quantiteVendue;

                if (ModelState.IsValid)
                {
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                    TempData["MontantTotal"] = montant.ToString(); // Convertir le montant en chaîne
                   
                }

                ModelState.AddModelError("Quantite", "La quantité à vendre dépasse la quantité en stock.");
                return View(produit);
            }
            else
            {
                ModelState.AddModelError("Quantite", "La quantité à vendre dépasse la quantité en stock.");
                return View(produit);
            }
        }
    }
   
}

