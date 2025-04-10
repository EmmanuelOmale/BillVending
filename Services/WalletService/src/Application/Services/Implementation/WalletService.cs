using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserWallet.Interfaces;
using Infrastructure.DatabaseContext;
using Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWalletLockProvider _lockProvider;
        private readonly ILogger<WalletService> _logger;

    public WalletService(ApplicationDbContext context, IWalletLockProvider lockProvider, ILogger<WalletService> logger)
    {
        _context = context;
        _lockProvider = lockProvider;
        _logger = logger;
    }

    public async Task<decimal> GetBalanceAsync(Guid userId)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId.ToString());

        if (wallet == null)
            throw new InvalidOperationException("Wallet not found.");

        return wallet.Balance;
    }

    public async Task FundWalletAsync(Guid userId, decimal amount)
    {
        var userLock = _lockProvider.GetLock(userId);
        await userLock.WaitAsync();

        try
        {
            const int maxRetry = 3;

            for (int attempt = 0; attempt < maxRetry; attempt++)
            {
                try
                {
                    var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId.ToString());

                    if (wallet == null)
                    {
                        wallet = new Wallet
                        {
                            UserId = userId.ToString(),
                            Balance = amount
                        };
                        _context.Wallets.Add(wallet);
                    }
                    else
                    {
                        wallet.Balance += amount;
                    }

                    await _context.SaveChangesAsync();
                    return;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogWarning("Concurrency issue while funding wallet for user {UserId} - Attempt {Attempt}", userId, attempt + 1);
                    if (attempt == maxRetry - 1)
                        throw new InvalidOperationException("Failed to fund wallet due to concurrency conflict.", ex);
                }
            }
        }
        finally
        {
            userLock.Release();
        }
    }

    public async Task DeductFromWalletAsync(Guid userId, decimal amount)
    {
        var userLock = _lockProvider.GetLock(userId);
        await userLock.WaitAsync();

        try
        {
            const int maxRetry = 3;

            for (int attempt = 0; attempt < maxRetry; attempt++)
            {
                try
                {
                    var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId.ToString());

                    if (wallet == null)
                        throw new InvalidOperationException("Wallet not found.");

                    if (wallet.Balance < amount)
                        throw new InvalidOperationException("Insufficient balance.");

                    wallet.Balance -= amount;

                    await _context.SaveChangesAsync();
                    return;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogWarning("Concurrency conflict on deduction for user {UserId}, attempt {Attempt}", userId, attempt + 1);
                    if (attempt == maxRetry - 1)
                        throw new InvalidOperationException("Failed to deduct wallet due to concurrency conflict.", ex);
                }
            }
        }
        finally
        {
            userLock.Release();
        }
    }

    public async Task RefundToWalletAsync(Guid userId, decimal amount)
    {
        var userLock = _lockProvider.GetLock(userId);
        await userLock.WaitAsync();

        try
        {
            const int maxRetry = 3;

            for (int attempt = 0; attempt < maxRetry; attempt++)
            {
                try
                {
                    var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId.ToString());

                    if (wallet == null)
                        throw new InvalidOperationException("Wallet not found.");

                    wallet.Balance += amount;

                    await _context.SaveChangesAsync();
                    return;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogWarning("Concurrency issue on refund for user {UserId}, attempt {Attempt}", userId, attempt + 1);
                    if (attempt == maxRetry - 1)
                        throw new InvalidOperationException("Failed to refund wallet due to concurrency conflict.", ex);
                }
            }
        }
        finally
        {
            userLock.Release();
        }
    }
    }
}