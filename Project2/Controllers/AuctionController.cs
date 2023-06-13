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
    public class AuctionController : Controller
    {
        private readonly AppDbContext _context;

        public AuctionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Auction
        [HttpGet("Auction")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Auctions.Include(a => a.Marketer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Auction/Details/5
        [HttpGet("Auction/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Auctions == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions
                .Include(a => a.Marketer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // GET: Auction/Create
        [HttpGet("Auction/Create")]
        public IActionResult Create()
        {
            ViewData["MarketerId"] = new SelectList(_context.Marketers, "Id", "Id");
            return View();
        }

        // POST: Auction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Auction/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Discretion,MarketerId,Value,Status,CreatedDate,FinalDate")] Auction auction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarketerId"] = new SelectList(_context.Marketers, "Id", "Id", auction.MarketerId);
            return View(auction);
        }

        // GET: Auction/Edit/5
        [HttpGet("Auction/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Auctions == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            ViewData["MarketerId"] = new SelectList(_context.Marketers, "Id", "Id", auction.MarketerId);
            return View(auction);
        }

        // POST: Auction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Auction/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Discretion,MarketerId,Value,Status,CreatedDate,FinalDate")] Auction auction)
        {
            if (id != auction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.Id))
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
            ViewData["MarketerId"] = new SelectList(_context.Marketers, "Id", "Id", auction.MarketerId);
            return View(auction);
        }

        // GET: Auction/Delete/5
        [HttpGet("Auction/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Auctions == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions
                .Include(a => a.Marketer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auction/Delete/5
        [HttpPost("Auction/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Auctions == null)
            {
                return Problem("Entity set 'AppDbContext.Auctions'  is null.");
            }
            var auction = await _context.Auctions.FindAsync(id);
            if (auction != null)
            {
                _context.Auctions.Remove(auction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionExists(int id)
        {
          return (_context.Auctions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
