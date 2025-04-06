using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.UserWallet.Repository
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByIdAsync(string id);
        Task<Wallet?> GetByUserIdAsync(string userId);
        Task<List<Wallet>> GetAllAsync();
        Task AddAsync(Wallet wallet);
        void Update(Wallet wallet);
        void Delete(Wallet wallet);
    }
}