using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Common;

namespace Common.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        // private readonly ICurrentUserService _currentUserService;
        // public AuditableEntitySaveChangesInterceptor (ICurrentUserService currentUserService)
        // {
        //     _currentUserService = currentUserService;
        // }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
                                                                            DbContextEventData eventData,
                                                                            InterceptionResult<int> result,
                                                                            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
                return new ValueTask<InterceptionResult<int>>(result);

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "System";
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "System";
                        break;

                    case EntityState.Deleted:
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.Entity.DeletedBy = "System";
                        break;
                }
            }

            return new ValueTask<InterceptionResult<int>>(result);
        }

    }
}