using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dto.Requests
{
    public class BillPaymentRequest
    {
        public Guid UserId { get; set; }
        public string MeterNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Disco { get; set; } = string.Empty;
    }
}