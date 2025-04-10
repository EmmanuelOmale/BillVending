using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Application.Dto.Requests;
using Application.Dto.Responses;

namespace Application.Services.Implementation
{
    public class BillVendorService : IBillVendorService
    {
        public async Task<BillPaymentResponse> ProcessPaymentAsync(BillPaymentRequest request)
    {
        await Task.Delay(1000);

        var random = new Random();
        var success = random.Next(1, 100) <= 75;

        return new BillPaymentResponse
        {
            IsSuccess = success,
            TransactionId = Guid.NewGuid().ToString()
        };
    }
    }
}