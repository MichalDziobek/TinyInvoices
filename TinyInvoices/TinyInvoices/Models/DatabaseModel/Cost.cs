using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInvoices.Models.DatabaseModel
{
    public class Cost
    {
        public int CostId { get; set; }
        public int UserGroupId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime StartingDate { get; set; }
        public TimeSpan? Interval { get; set; }
        public bool IsActive { get; set; }
        public bool IsRepeatable { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
