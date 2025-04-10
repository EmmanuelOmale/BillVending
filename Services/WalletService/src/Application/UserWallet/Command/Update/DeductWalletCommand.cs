using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserWallet.Interfaces;
using MediatR;

namespace Application.UserWallet.Command.Update
{
    public record DeductWalletCommand(Guid UserId, decimal Amount) : IRequest<Unit>;

    public class DeductWalletCommandHandler : IRequestHandler<DeductWalletCommand, Unit>
    {
        private readonly IWalletService _walletService;

        public DeductWalletCommandHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<Unit> Handle(DeductWalletCommand request, CancellationToken cancellationToken)
        {
            await _walletService.DeductFromWalletAsync(request.UserId, request.Amount);
            return Unit.Value;
        }
    }
}