using GLS.Platform.u202311077.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Assignments.Domain.Model.Queries;
using GLS.Platform.u202311077.Assignments.Domain.Services;
using GLS.Platform.u202311077.Shared.Domain.Repositories;

namespace GLS.Platform.u202311077.Assignments.Application.Internal.QueryServices;

/// <summary>
///     Implementation of the Device Query Service.
/// </summary>
/// <author>Ayrton Llanos</author>
public class DeviceQueryService(IBaseRepository<Device> deviceRepository) : IDeviceQueryService
{
    public async Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query)
    {
        return await deviceRepository.ListAsync();
    }
}