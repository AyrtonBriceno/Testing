using GLS.Platform.u202311077.Tracking.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Tracking.Domain.Model.Commands;

namespace GLS.Platform.u202311077.Tracking.Domain.Services;

/// <summary>
///     Interface for the Data Record Command Service.
/// </summary>
/// <author>Ayrton Llanos</author>
public interface IDataRecordCommandService
{
    Task<DataRecord> Handle(CreateDataRecordCommand command);
}