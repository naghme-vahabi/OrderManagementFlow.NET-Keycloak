using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OrderService.Infrastructure.Persistence.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext context)
        {
            if (context == null) return;

            var now = DateTime.Now;
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).ToString() ?? "System";

            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = now;
                    entry.Entity.CreateBy = userId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateDate = now;
                    entry.Entity.UpdateBy = userId;
                }
            }
        }
    }
}
