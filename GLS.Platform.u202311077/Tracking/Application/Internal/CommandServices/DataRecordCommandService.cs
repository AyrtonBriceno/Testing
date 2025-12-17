using GLS.Platform.u202311077.Shared.Domain.Model.Events;
using GLS.Platform.u202311077.Shared.Domain.Repositories;
using GLS.Platform.u202311077.Tracking.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Tracking.Domain.Model.Commands;
using GLS.Platform.u202311077.Tracking.Domain.Services;
using GLS.Platform.u202311077.Tracking.Domain.Services.External;


namespace GLS.Platform.u202311077.Tracking.Application.Internal.CommandServices;

/// <summary>
///     Implementation of the Data Record Command Service.
/// </summary>
/// <author>Ayrton Llanos</author>
public class DataRecordCommandService(
    IBaseRepository<DataRecord> dataRecordRepository,
    IUnitOfWork unitOfWork,
    IAssignmentsContextFacade assignmentsFacade,
    IPublisher publisher) // Inyectamos el Publisher de Cortex
    : IDataRecordCommandService
{
    public async Task<DataRecord> Handle(CreateDataRecordCommand command)
    {
        // 1. Validar existencia vía ACL
        if (!await assignmentsFacade.ExistsDeviceByMacAddress(command.DeviceMacAddress))
        {
            throw new ArgumentException($"Device with MAC {command.DeviceMacAddress} not found.");
        }

        // 2. Crear entidad
        var dataRecord = new DataRecord(
            command.DeviceMacAddress,
            command.OperationMode,
            command.TargetThrust,
            command.CurrentThrust,
            command.EngineState,
            command.GeneratedAt
        );

        // 3. Guardar
        await dataRecordRepository.AddAsync(dataRecord);
        await unitOfWork.CompleteAsync();

        // 4. Publicar Evento (Regla 45)
        await publisher.Publish(new DataRecordRegisteredEvent(dataRecord.DeviceMacAddress.Address, dataRecord.TargetThrust));

        return dataRecord;
    }
}