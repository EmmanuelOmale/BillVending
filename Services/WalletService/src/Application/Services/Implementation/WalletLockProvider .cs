using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using System.Collections.Concurrent;

namespace Application.Services.Implementation
{
    public class WalletLockProvider : IWalletLockProvider
    {
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _locks = new();

        public SemaphoreSlim GetLock(Guid userId)
        {
            return _locks.GetOrAdd(userId, _ => new SemaphoreSlim(1, 1));
        }
    }
}