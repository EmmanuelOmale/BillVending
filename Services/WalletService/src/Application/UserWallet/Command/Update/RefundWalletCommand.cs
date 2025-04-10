using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserWallet.Interfaces;
using MediatR;

namespace Application.UserWallet.Command.Update
{
    public record RefundWalletCommand(Guid UserId, decimal Amount) : IRequest<Unit>;

    public class RefundWalletCommandHandler : IRequestHandler<RefundWalletCommand, Unit>
    {
        private readonly IWalletService _walletService;

        public RefundWalletCommandHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<Unit> Handle(RefundWalletCommand request, CancellationToken cancellationToken)
        {
            await _walletService.RefundToWalletAsync(request.UserId, request.Amount);
            return Unit.Value;
        }
    }

}