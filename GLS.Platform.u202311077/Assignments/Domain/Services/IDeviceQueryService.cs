using GLS.Platform.u202311077.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Assignments.Domain.Model.Queries;

namespace GLS.Platform.u202311077.Assignments.Domain.Services;

/// <summary>
///     Interface for the Device Query Service.
/// </summary>
/// <author>Ayrton Llanos</author>
public interface IDeviceQueryService
{
    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query);
}