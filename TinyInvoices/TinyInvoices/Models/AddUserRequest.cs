using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInvoices.Models
{
    public class AddUserRequest
    {
        public string Email { get; set; }
        public int UserGroupId { get; set; }
    }
}
