using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Application.Services.Interfaces
{
    public interface IWalletLockProvider
    {
        SemaphoreSlim GetLock(Guid userId);
    }
}