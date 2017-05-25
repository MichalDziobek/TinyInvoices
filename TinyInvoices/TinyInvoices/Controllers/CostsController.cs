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
    public class CostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Costs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Costs.Include(c => c.UserGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Costs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .Include(c => c.UserGroup)
                .SingleOrDefaultAsync(m => m.CostId == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Costs/Create
        public IActionResult Create()
        {
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "UserGroupId", "UserGroupId");
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostId,UserGroupId,Name,Value,StartingDate,Interval,IsActive,IsRepeatable")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "UserGroupId", "UserGroupId", cost.UserGroupId);
            return View(cost);
        }

        // GET: Costs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs.SingleOrDefaultAsync(m => m.CostId == id);
            if (cost == null)
            {
                return NotFound();
            }
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "UserGroupId", "UserGroupId", cost.UserGroupId);
            return View(cost);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CostId,UserGroupId,Name,Value,StartingDate,Interval,IsActive,IsRepeatable")] Cost cost)
        {
            if (id != cost.CostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostExists(cost.CostId))
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
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "UserGroupId", "UserGroupId", cost.UserGroupId);
            return View(cost);
        }

        // GET: Costs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .Include(c => c.UserGroup)
                .SingleOrDefaultAsync(m => m.CostId == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cost = await _context.Costs.SingleOrDefaultAsync(m => m.CostId == id);
            _context.Costs.Remove(cost);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CostExists(int id)
        {
            return _context.Costs.Any(e => e.CostId == id);
        }
    }
}
