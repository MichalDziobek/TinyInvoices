using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInvoices.Models.DatabaseModel
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int UserGroupId { get; set; }
        public DateTime CalculationDate { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        public virtual ICollection<Charge> Charges { get; set; }
    }
}
