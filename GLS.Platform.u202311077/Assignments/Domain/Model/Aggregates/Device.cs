using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using GLS.Platform.u202311077.Shared.Domain.Model.ValueObjects;

namespace GLS.Platform.u202311077.Assignments.Domain.Model.Aggregates;

/// <summary>
///     Represents a Device aggregate root in the Assignments Bounded Context.
/// </summary>
/// <author>Ayrton Llanos</author>
public class Device : IEntityWithCreatedUpdatedDate
{
    public int Id { get; set; }
    public MacAddress MacAddress { get; private set; }
    public int MissionId { get; private set; }
    public decimal PreferredThrust { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }

    public Device()
    {
        MacAddress = new MacAddress();
    }

    public Device(string macAddress, int missionId, decimal preferredThrust)
    {
        MacAddress = new MacAddress(macAddress);
        MissionId = missionId;
        PreferredThrust = preferredThrust;
    }
}