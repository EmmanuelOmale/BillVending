using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Application.Dto.Requests;
using Application.UserWallet.Command.Update;
using Application.Dto.Responses;


namespace Application.UserWallet.Command.Create
{
    public record PayBillCommand(Guid UserId, decimal Amount, string MeterNumber, string Disco) : IRequest<BillPaymentResponse>;

    public class PayBillResult
    {
        public bool IsSuccess { get; set; }
        public string TransactionId { get; set; } = string.Empty;
    }

    public class PayBillCommandHandler : IRequestHandler<PayBillCommand, BillPaymentResponse>
    {
        private readonly IMediator _mediator;
        private readonly IBillVendorService _vendorService;
        private readonly ILogger<PayBillCommandHandler> _logger;

        public PayBillCommandHandler(IMediator mediator, IBillVendorService vendorService, ILogger<PayBillCommandHandler> logger)
        {
            _mediator = mediator;
            _vendorService = vendorService;
            _logger = logger;
        }

        public async Task<BillPaymentResponse> Handle(PayBillCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeductWalletCommand(request.UserId, request.Amount));

            var result = await _vendorService.ProcessPaymentAsync(new BillPaymentRequest
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Disco = request.Disco,
                MeterNumber = request.MeterNumber
            });

            if (!result.IsSuccess)
            {
                _ = Task.Run(async () =>
                {
                    await _mediator.Send(new RefundWalletCommand(request.UserId, request.Amount));
                    _logger.LogWarning("Refund triggered for failed payment: User {UserId}", request.UserId);
                });
            }

            return result;
        }
    }
}