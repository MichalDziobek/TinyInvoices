using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyInvoices.Data;
using TinyInvoices.Models.DatabaseModel;

namespace TinyInvoices.Controllers
{
    public class InvoicesCalculator
    {
        private readonly ApplicationDbContext _context;

        public InvoicesCalculator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CalculateInvoice(int userGroupId)
        {
            var costs = await _context.Costs
                                        .Include(x => x.Charges)
                                            .ThenInclude(x => x.Invoice)
                                        .Where(x => x.IsActive
                                                 && x.UserGroupId == userGroupId)
                                        .ToListAsync();
            var costDictionary = costs.ToDictionary(x => x, x => x.Charges.Any() ? x.Charges.Select(y => y.Invoice.CalculationDate).Max() : x.StartingDate);

            var userMappings = await _context.UserToGroupMappings
                                    .Where(x => x.UserGroupId == userGroupId)
                                    .ToListAsync();

            var invoice = new Invoice
            {
                CalculationDate = DateTime.Now,
                UserGroupId = userGroupId
            };

            foreach (var cost in costs.Where(x => !x.IsRepeatable))
            {
                cost.IsActive = false;
            }

            var charges = costDictionary.Select(x => new Charge
            {
                Cost = x.Key,
                Invoice = invoice,
                Value = GetValue(x)
            }).ToList();

            var bills = charges.Select(charge => userMappings
                                    .Select(mapping => new Bill
                                    {
                                        Charge = charge,
                                        UserToGroupMapping = mapping,
                                        Value = GetBillValue(charge,mapping, userMappings.Count)
                                    }))
                                    .SelectMany(x => x)
                                    .ToList();

            await _context.AddRangeAsync(bills);
            await _context.AddRangeAsync(charges);
            await _context.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        private decimal GetBillValue(Charge charge, UserToGroupMapping mapping, int mappingsCount)
        {
            return Math.Round(charge.Value / mappingsCount,2);
        }

        private decimal GetValue(KeyValuePair<Cost, DateTime> x)
        {
            return x.Key.Value * (x.Key.IsRepeatable ? TimeElapsedMultiplier(x) : 1);
        }

        private decimal TimeElapsedMultiplier(KeyValuePair<Cost, DateTime> x)
        {
            return Math.Floor((decimal)((DateTime.Now.Ticks - x.Value.Ticks) / x.Key.Interval.Value.Ticks));
        }
    }
}
