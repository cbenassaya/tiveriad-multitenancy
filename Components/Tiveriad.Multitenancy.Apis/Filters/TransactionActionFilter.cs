using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Services;

namespace Tiveriad.Multitenancy.Api.Filters;
public class TransactionActionFilter : IAsyncActionFilter
{
    private readonly DbContext _context;
    private readonly IUserManagerService _userManagerService;
    private readonly ILogger <TransactionActionFilter> _logger;
    public TransactionActionFilter(DbContext context, IUserManagerService userManagerService, ILogger <TransactionActionFilter> logger)
    {
        _context = context;
        _userManagerService = userManagerService;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //await using var tx = await _context.Database.BeginTransactionAsync();
        var result = await next();
        if (result.Exception == null || result.ExceptionHandled)
        {
            foreach (var entry in _context.ChangeTracker.Entries<IAuditable<string>>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _userManagerService.GetUserId();
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _userManagerService.GetUserId();
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            
            
            
            foreach (var entry in _context.ChangeTracker.Entries<IAuditable<string>>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _userManagerService.GetUserId();
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _userManagerService.GetUserId();
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Try to save changes", e);
                throw;
            }
           
        //await tx.CommitAsync();
        }
    }
}