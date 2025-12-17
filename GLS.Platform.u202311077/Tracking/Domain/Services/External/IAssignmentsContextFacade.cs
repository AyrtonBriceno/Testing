namespace GLS.Platform.u202311077.Tracking.Domain.Services.External;

/// <summary>
///     ACL Interface to interact with Assignments Context.
/// </summary>
/// <author>Ayrton Llanos</author>
public interface IAssignmentsContextFacade
{
    Task<bool> ExistsDeviceByMacAddress(string macAddress);
}