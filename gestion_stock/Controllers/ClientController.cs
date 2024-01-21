using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestion_stock.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class ClientController : Controller
{
    private readonly gestion_stockDbContext _context;

    public ClientController(gestion_stockDbContext context)
    {
        _context = context;
    }

    // GET: ClientController
    public async Task<IActionResult> Index(string searchNom)
    {
        var clients = from c in _context.Clients select c;

        if (!String.IsNullOrEmpty(searchNom))
        {
            clients = clients.Where(s => s.Nom.Contains(searchNom));
        }

        return View(await clients.ToListAsync());
    }

    // GET: ClientController/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.Id == id);

        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // GET: ClientController/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ClientController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Address,Cin,Tel")] Client client)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la création du client : {ex.Message}";
            return View(client);
        }
    }

    // GET: ClientController/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: ClientController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Address,Cin,Tel")] Client client)
    {
        try
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la modification du client : {ex.Message}";
            return View(client);
        }
    }

    // GET: ClientController/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.Id == id);

        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: ClientController/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la suppression du client : {ex.Message}";
            return View();
        }
    }
}
