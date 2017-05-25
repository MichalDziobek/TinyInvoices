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
    public class BillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bills.Include(b => b.Charge).Include(b => b.UserToGroupMapping);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Charge)
                .Include(b => b.UserToGroupMapping)
                .SingleOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId");
            ViewData["UserToGroupMappingId"] = new SelectList(_context.UserToGroupMappings, "UserToGroupMappingId", "UserToGroupMappingId");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillId,ChargeId,UserToGroupMappingId,Value")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId", bill.ChargeId);
            ViewData["UserToGroupMappingId"] = new SelectList(_context.UserToGroupMappings, "UserToGroupMappingId", "UserToGroupMappingId", bill.UserToGroupMappingId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.SingleOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId", bill.ChargeId);
            ViewData["UserToGroupMappingId"] = new SelectList(_context.UserToGroupMappings, "UserToGroupMappingId", "UserToGroupMappingId", bill.UserToGroupMappingId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillId,ChargeId,UserToGroupMappingId,Value")] Bill bill)
        {
            if (id != bill.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.BillId))
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
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId", bill.ChargeId);
            ViewData["UserToGroupMappingId"] = new SelectList(_context.UserToGroupMappings, "UserToGroupMappingId", "UserToGroupMappingId", bill.UserToGroupMappingId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Charge)
                .Include(b => b.UserToGroupMapping)
                .SingleOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.SingleOrDefaultAsync(m => m.BillId == id);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }
    }
}
