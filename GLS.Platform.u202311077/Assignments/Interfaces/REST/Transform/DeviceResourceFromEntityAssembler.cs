using GLS.Platform.u202311077.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Assignments.Interfaces.REST.Resources;

namespace GLS.Platform.u202311077.Assignments.Interfaces.REST.Transform;

/// <summary>
///     Assembler to convert Device entities into Device resources.
/// </summary>
/// <author>Ayrton Llanos</author>
public static class DeviceResourceFromEntityAssembler
{
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        return new DeviceResource(
            entity.Id,
            entity.MacAddress.ToString(),
            entity.MissionId,
            entity.PreferredThrust
        );
    }
}