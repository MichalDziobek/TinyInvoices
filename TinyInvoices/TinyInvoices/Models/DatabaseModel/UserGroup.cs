using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInvoices.Models.DatabaseModel
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FirstAutomaticInvoiceGenerationDate { get; set; }
        public TimeSpan? AutomaticInvoiceInterval { get; set; }

        public virtual ICollection<UserToGroupMapping> UserToGroupMappings { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
