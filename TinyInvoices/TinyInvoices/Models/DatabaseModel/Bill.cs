using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInvoices.Models.DatabaseModel
{
    public class Bill
    {
        public int BillId { get; set; }
        public int ChargeId { get; set; }
        public int UserToGroupMappingId { get; set; }
        public decimal Value { get; set; }

        public virtual Charge Charge { get; set; }
        public virtual UserToGroupMapping UserToGroupMapping { get; set; }
    }
}
