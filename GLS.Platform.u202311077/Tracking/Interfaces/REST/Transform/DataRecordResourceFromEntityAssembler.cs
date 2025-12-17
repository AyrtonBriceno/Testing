using GLS.Platform.u202311077.Tracking.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Tracking.Interfaces.REST.Resources;

namespace GLS.Platform.u202311077.Tracking.Interfaces.REST.Transform;

/// <summary>
///     Assembler to convert DataRecord entity to DataRecordResource.
/// </summary>
/// <author>Ayrton Llanos</author>
public static class DataRecordResourceFromEntityAssembler
{
    public static DataRecordResource ToResourceFromEntity(DataRecord entity)
    {
        return new DataRecordResource(
            entity.Id,
            entity.DeviceMacAddress.ToString(),
            entity.OperationMode.ToString(),
            entity.TargetThrust,
            entity.CurrentThrust,
            entity.EngineState.ToString(),
            entity.GeneratedAt.ToString("yyyy-MM-dd HH:mm:ss") 
        );
    }
}