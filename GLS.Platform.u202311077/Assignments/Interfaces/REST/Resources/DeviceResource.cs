namespace GLS.Platform.u202311077.Assignments.Interfaces.REST.Resources;

/// <summary>
///     Represents the data structure for a Device resource in the API response.
/// </summary>
/// <author>Ayrton Llanos</author>
public record DeviceResource(int Id, string MacAddress, int MissionId, decimal PreferredThrust);