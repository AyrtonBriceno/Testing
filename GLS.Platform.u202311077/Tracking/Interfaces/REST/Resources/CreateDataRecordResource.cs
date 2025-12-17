namespace GLS.Platform.u202311077.Tracking.Interfaces.REST.Resource;

/// <summary>
///     Resource to create a data record.
/// </summary>
/// <author>Ayrton Llanos</author>
public record CreateDataRecordResource(
    string DeviceMacAddress,
    string OperationMode,
    decimal TargetThrust,
    decimal CurrentThrust,
    string EngineState,
    string GeneratedAt
);