using System.Net.Mime;
using GLS.Platform.u202311077.Assignments.Domain.Model.Queries;
using GLS.Platform.u202311077.Assignments.Domain.Services;
using GLS.Platform.u202311077.Assignments.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace GLS.Platform.u202311077.Assignments.Interfaces.REST;

/// <summary>
///     Controller for managing Device resources.
/// </summary>
/// <author>Ayrton Llanos</author>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class DevicesController(IDeviceQueryService deviceQueryService) : ControllerBase
{
    /// <summary>
    ///     Get all devices.
    /// </summary>
    /// <returns>A list of device resources.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllDevices()
    {
        var devices = await deviceQueryService.Handle(new GetAllDevicesQuery());
        var resources = devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}