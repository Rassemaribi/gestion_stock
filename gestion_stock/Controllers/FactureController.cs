using gestion_stock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace gestion_stock.Controllers
{
    public class FactureController : Controller
    {
        private readonly gestion_stockDbContext _context;

        public FactureController(gestion_stockDbContext context)
        {
            _context = context;
        }

        // GET: FactureController
        public IActionResult Index(string search)
        {
            List<Facture> factures;
            if (!string.IsNullOrEmpty(search))
            {
                factures = _context.Facture
                    .Include(x => x.Produit)
                    .Include(x => x.Client)
                    .Where(f => f.Client.Nom.Contains(search))
                    .ToList();
            }
            else
            {
                factures = _context.Facture
                    .Include(x => x.Produit)
                    .Include(x => x.Client)
                    .ToList();
            }

            return View(factures);
        }



        // GET: FactureController/Details/5
        public ActionResult Details(int id)
        {
            var facture = _context.Facture.FirstOrDefault(f => f.IdFacture == id);
            return View(facture);
        }

        // GET: FactureController/Create
        // GET: FactureController/Create
        public ActionResult Create()
        {
            ViewBag.ProduitList = _context.Produits.ToList();
            ViewBag.ClientList = _context.Clients.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Facture facture)
        {
            try
            {
                if (ModelState.IsValid)
                {
                
                    
                     
                    facture.MontantTotalAchat =(double) (((facture.NombrePiecesAchete) * (facture.Prix)));
                   
                        _context.Facture.Add(facture);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));


                   
                }

                ViewBag.ProduitList = _context.Produits.ToList();
                ViewBag.ClientList = _context.Clients.ToList();
                return View(facture);
            }
            catch
            {
                return View();
            }
        }


        // GET: FactureController/Edit/5
        public ActionResult Edit(int id)
        {
            var facture = _context.Facture.FirstOrDefault(f => f.IdFacture == id);

            if (facture == null)
            {
                return NotFound();
            }

            ViewBag.ProduitList = _context.Produits.ToList();
            ViewBag.ClientList = _context.Clients.ToList();
            return View(facture);
        }

        // POST: FactureController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Facture facture)
        {
            if (id != facture.IdFacture)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facture);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(facture.IdFacture))
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

            ViewBag.ProduitList = _context.Produits.ToList();
            ViewBag.ClientList = _context.Clients.ToList();
            return View(facture);
        }

        // GET: FactureController/Delete/5
        public ActionResult Delete(int id)
        {
            var facture = _context.Facture.FirstOrDefault(f => f.IdFacture == id);
            return View(facture);
        }

        // POST: FactureController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var facture = _context.Facture.FirstOrDefault(f => f.IdFacture == id);
            _context.Facture.Remove(facture);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool FactureExists(int id)
        {
            return _context.Facture.Any(f => f.IdFacture == id);
        }
    }
}
