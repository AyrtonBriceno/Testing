using GLS.Platform.u202311077.Shared.Infrastructure.Persistence.EFC.Configuration;
using GLS.Platform.u202311077.Tracking.Domain.Services.External;
using Microsoft.EntityFrameworkCore;

namespace GLS.Platform.u202311077.Tracking.Infrastructure.Services;

/// <summary>
///     ACL Implementation to interact with Assignments Context.
/// </summary>
/// <author>Ayrton Llanos</author>
public class AssignmentsContextFacade(AppDbContext context) : IAssignmentsContextFacade
{
    public async Task<bool> ExistsDeviceByMacAddress(string macAddress)
    {
        // Consultamos la tabla de Devices directamete para verificar existencia
        // Nota: MacAddress es un Value Object, EF Core lo mapea como owned type
        return await context.Devices.AnyAsync(d => d.MacAddress.Address == macAddress);
    }
}