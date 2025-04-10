using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserWallet.Interfaces;
using MediatR;

namespace Application.UserWallet.Command.Update
{
    public record FundWalletCommand(Guid UserId, decimal Amount) : IRequest<Unit>;
    public class FundWalletCommandHandler : IRequestHandler<FundWalletCommand, Unit>
    {
        private readonly IWalletService _walletService;

        public FundWalletCommandHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<Unit> Handle(FundWalletCommand request, CancellationToken cancellationToken)
        {
            await _walletService.FundWalletAsync(request.UserId, request.Amount);
            return Unit.Value;
        }
    }
}