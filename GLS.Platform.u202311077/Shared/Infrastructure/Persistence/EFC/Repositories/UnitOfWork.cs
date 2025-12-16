using GLS.Platform.u202311077.Shared.Domain.Repositories;
using GLS.Platform.u202311077.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace GLS.Platform.u202311077.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Unit of work for the application.
/// </summary>
/// <remarks>
///     This class is used to save changes to the database context.
///     It implements the IUnitOfWork interface.
/// </remarks>
/// <param name="context">
///     The database context for the application
/// </param>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    // inheritedDoc
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}