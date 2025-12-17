using System.Net.Mime;
using GLS.Platform.u202311077.Tracking.Domain.Services;
using GLS.Platform.u202311077.Tracking.Interfaces.REST.Resource;
using GLS.Platform.u202311077.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace GLS.Platform.u202311077.Tracking.Interfaces.REST;

/// <summary>
///     Controller for managing Data Record resources.
/// </summary>
/// <param name="dataRecordCommandService">The service to handle data record commands.</param>
/// <author>Ayrton Llanos</author>
[ApiController]
[Route("api/v1/data-records")] // Kebab-case explícito por si acaso
[Produces(MediaTypeNames.Application.Json)]
public class DataRecordsController(IDataRecordCommandService dataRecordCommandService) : ControllerBase
{
    /// <summary>
    ///     Creates a new data record.
    /// </summary>
    /// <param name="resource">The resource containing the data record information.</param>
    /// <returns>The created data record resource.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateDataRecord([FromBody] CreateDataRecordResource resource)
    {
        var command = CreateDataRecordCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var dataRecord = await dataRecordCommandService.Handle(command);
        
        var responseResource = DataRecordResourceFromEntityAssembler.ToResourceFromEntity(dataRecord);
        
        // Retornamos 201 Created
        return CreatedAtAction(nameof(CreateDataRecord), new { id = dataRecord.Id }, responseResource);
    }
}