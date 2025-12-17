using GLS.Platform.u202311077.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Shared.Application.Internal.EventHandlers;
using GLS.Platform.u202311077.Shared.Domain.Model.Events;
using GLS.Platform.u202311077.Shared.Domain.Repositories;

namespace GLS.Platform.u202311077.Assignments.Application.Internal.EventHandlers;

/// <summary>
///     Event Handler to update Device PreferredThrust when a DataRecord is registered.
/// </summary>
/// <author>Ayrton Llanos</author>
public class DataRecordRegisteredEventHandler(IBaseRepository<Device> deviceRepository, IUnitOfWork unitOfWork) 
    : IEventHandler<DataRecordRegisteredEvent>
{
    public async Task Handle(DataRecordRegisteredEvent @event, CancellationToken cancellationToken)
    {
        // 1. Buscar el dispositivo por MAC (Tenemos que buscar en memoria o extender el repo, 
        // pero usando ListAsync y LINQ es la forma rápida para el examen)
        var allDevices = await deviceRepository.ListAsync();
        var device = allDevices.FirstOrDefault(d => d.MacAddress.Address == @event.DeviceMacAddress);

        if (device != null)
        {
            // 2. Regla 48: Actualizar PreferredThrust si es diferente al TargetThrust
            if (device.PreferredThrust != @event.TargetThrust)
            {
                device.PreferredThrust = @event.TargetThrust;
                
                await deviceRepository.UpdateAsync(device);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}