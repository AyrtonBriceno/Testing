namespace GLS.Platform.u202311077.Tracking.Domain.Model.Commands;

/// <summary>
///     Command to create a data record.
/// </summary>
/// <author>Ayrton Llanos</author>
public record CreateDataRecordCommand(
    string DeviceMacAddress,
    string OperationMode,
    decimal TargetThrust,
    decimal CurrentThrust,
    string EngineState,
    string GeneratedAt
);