using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInvoices.Models.DatabaseModel
{
    public class UserToGroupMapping
    {
        public int UserToGroupMappingId { get; set; }
        public string UserId { get; set; }
        public int UserGroupId { get; set; }
        public bool IsAdmin { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }

    }
}
