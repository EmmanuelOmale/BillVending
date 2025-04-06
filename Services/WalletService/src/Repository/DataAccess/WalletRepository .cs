using Microsoft.EntityFrameworkCore;
//using WalletService.Domain.Entities;
//using WalletService.Domain.Repositories;
using Domain;
using Infrastructure.DatabaseContext;

namespace Application.UserWallet.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetByIdAsync(string id) =>
            await _context.Wallets.FindAsync(id);

        public async Task<Wallet?> GetByUserIdAsync(string userId) =>
            await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);

        public async Task<List<Wallet>> GetAllAsync() =>
            await _context.Wallets.ToListAsync();

        public async Task AddAsync(Wallet wallet) =>
            await _context.Wallets.AddAsync(wallet);

        public void Update(Wallet wallet) =>
            _context.Wallets.Update(wallet);

        public void Delete(Wallet wallet) =>
            _context.Wallets.Remove(wallet);
    }
}
