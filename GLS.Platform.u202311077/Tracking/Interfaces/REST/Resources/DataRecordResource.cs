namespace GLS.Platform.u202311077.Tracking.Interfaces.REST.Resources;

/// <summary>
///     Resource representing a data record.
/// </summary>
/// <author>Ayrton Llanos</author>
public record DataRecordResource(
    int Id,
    string DeviceMacAddress,
    string OperationMode,
    decimal TargetThrust,
    decimal CurrentThrust,
    string EngineState,
    string GeneratedAt
);