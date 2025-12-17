using System.Globalization;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using GLS.Platform.u202311077.Shared.Domain.Model.ValueObjects;
using GLS.Platform.u202311077.Tracking.Domain.Model.ValueObjects;

namespace GLS.Platform.u202311077.Tracking.Domain.Model.Aggregates;

/// <summary>
///     Represents a Data Record aggregate root in the Tracking Bounded Context.
/// </summary>
/// <author>Ayrton Llanos</author>
public class DataRecord : IEntityWithCreatedUpdatedDate
{
    public int Id { get; set; }
    public MacAddress DeviceMacAddress { get; private set; }
    public EOperationMode OperationMode { get; private set; }
    public decimal TargetThrust { get; private set; }
    public decimal CurrentThrust { get; private set; }
    public EEngineState EngineState { get; private set; }
    public DateTime GeneratedAt { get; private set; }

    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }

    // Constructor vacío para EF Core
    public DataRecord()
    {
        DeviceMacAddress = new MacAddress();
    }

    /// <summary>
    ///     Constructor with business logic validation.
    /// </summary>
    /// <param name="macAddress">The device MAC address.</param>
    /// <param name="operationMode">The operation mode as string.</param>
    /// <param name="targetThrust">The target thrust value.</param>
    /// <param name="currentThrust">The current thrust value.</param>
    /// <param name="engineState">The engine state as string.</param>
    /// <param name="generatedAt">The generated timestamp as string.</param>
    public DataRecord(string macAddress, string operationMode, decimal targetThrust, decimal currentThrust, string engineState, string generatedAt)
    {
        // Regla 38: TargetThrust entre 700.0 y 950.0
        if (targetThrust < 700.0m || targetThrust > 950.0m)
        {
            throw new ArgumentException("Target Thrust must be between 700.0 and 950.0");
        }

        // Regla 42: Parseo de fecha específico
        if (!DateTime.TryParseExact(generatedAt, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            throw new ArgumentException("Invalid Date Format. Expected: yyyy-MM-dd HH:mm:ss");
        }

        // Regla 41: Fecha no futura
        if (parsedDate > DateTime.Now)
        {
            throw new ArgumentException("GeneratedAt cannot be in the future");
        }

        // Regla 43: Parseo de Enums desde string
        if (!Enum.TryParse(operationMode, true, out EOperationMode opMode))
        {
            throw new ArgumentException($"Invalid Operation Mode: {operationMode}");
        }

        if (!Enum.TryParse(engineState, true, out EEngineState engState))
        {
            throw new ArgumentException($"Invalid Engine State: {engineState}");
        }

        DeviceMacAddress = new MacAddress(macAddress); // Esto valida el formato MAC internamente
        TargetThrust = targetThrust;
        CurrentThrust = currentThrust;
        OperationMode = opMode;
        EngineState = engState;
        GeneratedAt = parsedDate;
    }
}