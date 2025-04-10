using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dto.Responses
{
    public class BillPaymentResponse
    {
        public bool IsSuccess { get; set; }
        public string TransactionId { get; set; } = string.Empty;
    }
}