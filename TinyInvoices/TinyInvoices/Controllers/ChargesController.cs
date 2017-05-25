using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyInvoices.Data;
using TinyInvoices.Models.DatabaseModel;
using Microsoft.AspNetCore.Authorization;

namespace TinyInvoices.Controllers
{
    [Authorize]
    public class ChargesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChargesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Charges
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Charges.Include(c => c.Invoice);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Charges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charge = await _context.Charges
                .Include(c => c.Invoice)
                .SingleOrDefaultAsync(m => m.ChargeId == id);
            if (charge == null)
            {
                return NotFound();
            }

            return View(charge);
        }

        // GET: Charges/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId");
            return View();
        }

        // POST: Charges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChargeId,InvoiceId,RuleId,Value")] Charge charge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charge);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", charge.InvoiceId);
            return View(charge);
        }

        // GET: Charges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charge = await _context.Charges.SingleOrDefaultAsync(m => m.ChargeId == id);
            if (charge == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", charge.InvoiceId);
            return View(charge);
        }

        // POST: Charges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChargeId,InvoiceId,RuleId,Value")] Charge charge)
        {
            if (id != charge.ChargeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChargeExists(charge.ChargeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", charge.InvoiceId);
            return View(charge);
        }

        // GET: Charges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charge = await _context.Charges
                .Include(c => c.Invoice)
                .SingleOrDefaultAsync(m => m.ChargeId == id);
            if (charge == null)
            {
                return NotFound();
            }

            return View(charge);
        }

        // POST: Charges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var charge = await _context.Charges.SingleOrDefaultAsync(m => m.ChargeId == id);
            _context.Charges.Remove(charge);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChargeExists(int id)
        {
            return _context.Charges.Any(e => e.ChargeId == id);
        }
    }
}
