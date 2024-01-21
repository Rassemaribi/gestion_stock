using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestion_stock.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class FournisseurController : Controller
{
    private readonly gestion_stockDbContext _context;

    public FournisseurController(gestion_stockDbContext context)
    {
        _context = context;
    }

    // GET: Fournisseur
    public async Task<IActionResult> Index(string searchString)
    {
        var fournisseurs = from f in _context.Fournisseurs select f;

        if (!String.IsNullOrEmpty(searchString))
        {
            fournisseurs = fournisseurs.Where(f => f.Nom.Contains(searchString));
        }

        return View(await fournisseurs.ToListAsync());
    }


    // GET: Fournisseur/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fournisseur = await _context.Fournisseurs
            .FirstOrDefaultAsync(m => m.Id == id);

        if (fournisseur == null)
        {
            return NotFound();
        }

        return View(fournisseur);
    }

    // GET: Fournisseur/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Fournisseur/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nom,Prenom,NumTel,Adresse")] Fournisseur fournisseur)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(fournisseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fournisseur);
        }
        catch (Exception ex)
        {
            // Gérer les erreurs de manière appropriée, par exemple, journaliser l'erreur
            ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la création du fournisseur : {ex.Message}";
            return View(fournisseur);
        }
    }

    // GET: Fournisseur/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fournisseur = await _context.Fournisseurs.FindAsync(id);

        if (fournisseur == null)
        {
            return NotFound();
        }

        return View(fournisseur);
    }

    // POST: Fournisseur/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,NumTel,Adresse")] Fournisseur fournisseur)
    {
        try
        {
            if (id != fournisseur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(fournisseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fournisseur);
        }
        catch (Exception ex)
        {
            // Gérer les erreurs de manière appropriée, par exemple, journaliser l'erreur
            ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la modification du fournisseur : {ex.Message}";
            return View(fournisseur);
        }
    }

    // GET: Fournisseur/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fournisseur = await _context.Fournisseurs
            .FirstOrDefaultAsync(m => m.Id == id);

        if (fournisseur == null)
        {
            return NotFound();
        }

        return View(fournisseur);
    }

    // POST: Fournisseur/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            _context.Fournisseurs.Remove(fournisseur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Gérer les erreurs de manière appropriée, par exemple, journaliser l'erreur
            ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la suppression du fournisseur : {ex.Message}";
            return View();
        }
    }
}
