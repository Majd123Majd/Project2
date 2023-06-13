using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2.Model;
using Project2.Model.Entities;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PopularizationController : Controller
    {
        private readonly AppDbContext _context;

        public PopularizationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Popularization
        [HttpGet("Popularization")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Popularizations.Include(p => p.Marketer).Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Popularization/Details/5
        [HttpGet("Popularization/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Popularizations == null)
            {
                return NotFound();
            }

            var popularization = await _context.Popularizations
                .Include(p => p.Marketer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (popularization == null)
            {
                return NotFound();
            }

            return View(popularization);
        }

        // GET: Popularization/Create
        [HttpGet("Popularization/Create")]
        public IActionResult Create()
        {
            ViewData["marketerId"] = new SelectList(_context.Marketers, "Id", "Id");
            ViewData["productId"] = new SelectList(_context.Products, "id", "id");
            return View();
        }

        // POST: Popularization/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Popularization/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Discretion,marketerId,productId,CreatedDate")] Popularization popularization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(popularization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["marketerId"] = new SelectList(_context.Marketers, "Id", "Id", popularization.marketerId);
            ViewData["productId"] = new SelectList(_context.Products, "id", "id", popularization.productId);
            return View(popularization);
        }

        // GET: Popularization/Edit/5
        [HttpGet("Popularization/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Popularizations == null)
            {
                return NotFound();
            }

            var popularization = await _context.Popularizations.FindAsync(id);
            if (popularization == null)
            {
                return NotFound();
            }
            ViewData["marketerId"] = new SelectList(_context.Marketers, "Id", "Id", popularization.marketerId);
            ViewData["productId"] = new SelectList(_context.Products, "id", "id", popularization.productId);
            return View(popularization);
        }

        // POST: Popularization/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Popularization/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Discretion,marketerId,productId,CreatedDate")] Popularization popularization)
        {
            if (id != popularization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(popularization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PopularizationExists(popularization.Id))
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
            ViewData["marketerId"] = new SelectList(_context.Marketers, "Id", "Id", popularization.marketerId);
            ViewData["productId"] = new SelectList(_context.Products, "id", "id", popularization.productId);
            return View(popularization);
        }

        // GET: Popularization/Delete/5
        [HttpGet("Popularization/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Popularizations == null)
            {
                return NotFound();
            }

            var popularization = await _context.Popularizations
                .Include(p => p.Marketer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (popularization == null)
            {
                return NotFound();
            }

            return View(popularization);
        }

        // POST: Popularization/Delete/5
        [HttpPost("Popularization/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Popularizations == null)
            {
                return Problem("Entity set 'AppDbContext.Popularizations'  is null.");
            }
            var popularization = await _context.Popularizations.FindAsync(id);
            if (popularization != null)
            {
                _context.Popularizations.Remove(popularization);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PopularizationExists(int id)
        {
          return (_context.Popularizations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
