using GLS.Platform.u202311077.Tracking.Domain.Model.Commands;
using GLS.Platform.u202311077.Tracking.Interfaces.REST.Resource;

namespace GLS.Platform.u202311077.Tracking.Interfaces.REST.Transform;

/// <summary>
///     Assembler to convert CreateDataRecordResource to CreateDataRecordCommand.
/// </summary>
/// <author>Ayrton Llanos</author>
public static class CreateDataRecordCommandFromResourceAssembler
{
    public static CreateDataRecordCommand ToCommandFromResource(CreateDataRecordResource resource)
    {
        return new CreateDataRecordCommand(
            resource.DeviceMacAddress,
            resource.OperationMode,
            resource.TargetThrust,
            resource.CurrentThrust,
            resource.EngineState,
            resource.GeneratedAt
        );
    }
}