using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UserWallet.Interfaces
{
    public interface IWalletService
    {
        Task<decimal> GetBalanceAsync(Guid userId);
        Task FundWalletAsync(Guid userId, decimal amount);
        Task DeductFromWalletAsync(Guid userId, decimal amount);
        Task RefundToWalletAsync(Guid userId, decimal amount);
    }
}