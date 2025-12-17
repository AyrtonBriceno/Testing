namespace GLS.Platform.u202311077.Shared.Domain.Model.Events;

/// <summary>
///     Event published when a data record is registered.
/// </summary>
/// <author>Ayrton Llanos</author>
public record DataRecordRegisteredEvent(string DeviceMacAddress, decimal TargetThrust);