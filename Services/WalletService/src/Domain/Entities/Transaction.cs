using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace Domain.Entities
{
    public class Transaction : BaseAuditableEntity
    {
        public string UserId { get; set; }
        public string WalletId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}