using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserWallet.Interfaces;
using MediatR;

namespace Application.UserWallet.Queries
{
    public class GetBalanceQuery : IRequest<decimal>
    {
        public Guid UserId { get; set; }
    }

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, decimal>
    {
        private readonly IWalletService _walletService;

        public GetBalanceQueryHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<decimal> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            return await _walletService.GetBalanceAsync(request.UserId);
        }
    }
}