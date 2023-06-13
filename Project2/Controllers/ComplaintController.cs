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
    public class ComplaintController : Controller
    {
        private readonly AppDbContext _context;

        public ComplaintController(AppDbContext context)
        { 
            _context = context;
        }

        // GET: Complaint
        [HttpGet("Complaint")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Complaints.Include(c => c.ComplainedOn).Include(c => c.Complainer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Complaint/Details/5
        [HttpGet("Complaint/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.ComplainedOn)
                .Include(c => c.Complainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaint/Create
        [HttpGet("Complaint/Create")]
        public IActionResult Create()
        {
            ViewData["ComplainedOnId"] = new SelectList(_context.Users, "UID", "UID");
            ViewData["ComplainerId"] = new SelectList(_context.Users, "UID", "UID");
            return View();
        }

        // POST: Complaint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Complaint/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Discretion,ComplainerId,ComplainedOnId,complaintType,Status,CreatedDate")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplainedOnId"] = new SelectList(_context.Users, "UID", "UID", complaint.ComplainedOnId);
            ViewData["ComplainerId"] = new SelectList(_context.Users, "UID", "UID", complaint.ComplainerId);
            return View(complaint);
        }

        // GET: Complaint/Edit/5
        [HttpGet("Complaint/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["ComplainedOnId"] = new SelectList(_context.Users, "UID", "UID", complaint.ComplainedOnId);
            ViewData["ComplainerId"] = new SelectList(_context.Users, "UID", "UID", complaint.ComplainerId);
            return View(complaint);
        }

        // POST: Complaint/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Complaint/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Discretion,ComplainerId,ComplainedOnId,complaintType,Status,CreatedDate")] Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
            ViewData["ComplainedOnId"] = new SelectList(_context.Users, "UID", "UID", complaint.ComplainedOnId);
            ViewData["ComplainerId"] = new SelectList(_context.Users, "UID", "UID", complaint.ComplainerId);
            return View(complaint);
        }

        // GET: Complaint/Delete/5
        [HttpGet("Complaint/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.ComplainedOn)
                .Include(c => c.Complainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Complaints == null)
            {
                return Problem("Entity set 'AppDbContext.Complaints'  is null.");
            }
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
          return (_context.Complaints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
