using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInvoices.Models.DatabaseModel
{
    public class Charge
    {
        public int ChargeId { get; set; }
        public int InvoiceId { get; set; }
        public int CostId { get; set; }
        public decimal Value { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual Cost Cost { get; set; }

    }
}
