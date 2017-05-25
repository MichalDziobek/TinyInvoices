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
using Microsoft.AspNetCore.Identity;

namespace TinyInvoices.Controllers
{
    [Authorize]
    public class UserGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InvoicesCalculator _invoicesCalculator;


        public UserGroupsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            InvoicesCalculator invoicesCalculator)
        {
            _context = context;
            _userManager = userManager;
            _invoicesCalculator = invoicesCalculator;
        }

        // GET: UserGroups
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var groups = await _context.UserGroups
                .Where(x => x.UserToGroupMappings
                        .Select(y => y.UserId)
                        .Contains(userId))
                .ToListAsync();
            return View(groups);
        }

        public async Task<IActionResult> GenerateInvoice(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            await _invoicesCalculator.CalculateInvoice(id.Value);
            return RedirectToAction(nameof(UserGroupsController.Index));
        }

        // GET: UserGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups
                .SingleOrDefaultAsync(m => m.UserGroupId == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // GET: UserGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserGroupId,Name,Description,FirstAutomaticInvoiceGenerationDate,AutomaticInvoiceInterval")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var mapping = new UserToGroupMapping
                {
                    User = user,
                    IsAdmin = true,
                    UserGroup = userGroup
                };
                await _context.UserToGroupMappings.AddAsync(mapping);
                await _context.AddAsync(userGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userGroup);
        }

        // GET: UserGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups.SingleOrDefaultAsync(m => m.UserGroupId == id);
            if (userGroup == null)
            {
                return NotFound();
            }
            return View(userGroup);
        }

        // POST: UserGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserGroupId,Name,Description,FirstAutomaticInvoiceGenerationDate,AutomaticInvoiceInterval")] UserGroup userGroup)
        {
            if (id != userGroup.UserGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGroupExists(userGroup.UserGroupId))
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
            return View(userGroup);
        }

        // GET: UserGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups
                .SingleOrDefaultAsync(m => m.UserGroupId == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userGroup = await _context.UserGroups.SingleOrDefaultAsync(m => m.UserGroupId == id);
            _context.UserGroups.Remove(userGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserGroupExists(int id)
        {
            return _context.UserGroups.Any(e => e.UserGroupId == id);
        }
    }
}
