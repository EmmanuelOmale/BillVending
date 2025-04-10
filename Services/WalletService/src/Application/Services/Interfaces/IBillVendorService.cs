using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto.Requests;
using Application.Dto.Responses;

namespace Application.Services.Interfaces
{
    public interface IBillVendorService
    {
        Task<BillPaymentResponse> ProcessPaymentAsync(BillPaymentRequest request);
    }
}