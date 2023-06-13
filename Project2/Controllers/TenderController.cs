using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenderController : Controller
    {
        private readonly AppDbContext _context;
       
        public TenderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tender
        [HttpGet("Tender")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tenders.Include(t => t.Customer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Tender/Details/5
        [HttpGet("Tender/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tenders == null)
            {
                return NotFound();
            }

            var tender = await _context.Tenders
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tender == null)
            {
                return NotFound();
            }

            return View(tender);
        }

        // GET: Tender/Create
        [HttpGet("Tender/Create")]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: Tender/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Tender/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Discretion,CustomerId,Value,Status,CreatedDate,FinalDate")] Tender tender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", tender.CustomerId);
            return View(tender);
        }

        // GET: Tender/Edit/5
        [HttpGet("Tender/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tenders == null)
            {
                return NotFound();
            }

            var tender = await _context.Tenders.FindAsync(id);
            if (tender == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", tender.CustomerId);
            return View(tender);
        }

        // POST: Tender/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Tender/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Discretion,CustomerId,Value,Status,CreatedDate,FinalDate")] Tender tender)
        {
            if (id != tender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenderExists(tender.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", tender.CustomerId);
            return View(tender);
        }

        // GET: Tender/Delete/5
        [HttpGet("Tender/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tenders == null)
            {
                return NotFound();
            }

            var tender = await _context.Tenders
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tender == null)
            {
                return NotFound();
            }

            return View(tender);
        }

        // POST: Tender/Delete/5
        [HttpPost("Tender/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tenders == null)
            {
                return Problem("Entity set 'AppDbContext.Tenders'  is null.");
            }
            var tender = await _context.Tenders.FindAsync(id);
            if (tender != null)
            {
                _context.Tenders.Remove(tender);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenderExists(int id)
        {
          return (_context.Tenders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
