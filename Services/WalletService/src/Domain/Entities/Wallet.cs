using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace Domain.Entities
{
    public class Wallet : BaseAuditableEntity
    {
        public string UserId { get; set; }
        public decimal Balance { get; set; }
    }
}