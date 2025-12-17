using Cortex.Mediator.Extensions;
using GLS.Platform.u202311077.Assignments.Application.Internal.EventHandlers;
using GLS.Platform.u202311077.Assignments.Application.Internal.QueryServices;
using GLS.Platform.u202311077.Assignments.Domain.Services;
using GLS.Platform.u202311077.Shared.Domain.Repositories;
using GLS.Platform.u202311077.Shared.Infrastructure.Interfaces.ASP.Configuration;
using GLS.Platform.u202311077.Shared.Infrastructure.Persistence.EFC.Configuration;
using GLS.Platform.u202311077.Shared.Infrastructure.Persistence.EFC.Repositories;
using GLS.Platform.u202311077.Tracking.Application.Internal.CommandServices;
using GLS.Platform.u202311077.Tracking.Domain.Services;
using GLS.Platform.u202311077.Tracking.Domain.Services.External;
using GLS.Platform.u202311077.Tracking.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Kebab-case para rutas
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// 2. Database Connection (MySQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// VERIFICAR: Asegúrate que tu appsettings.json tenga una cadena válida, si no, usa esta por defecto:
if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = "server=localhost;uid=root;pwd=root;database=gls_platform_u202311077;error details=true";
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (connectionString != null)
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
});

// 3. OpenAPI / Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GLS Platform API",
        Version = "v1",
        Description = "API for AeroSpace Dynamics - GLS Platform",
        Contact = new OpenApiContact { Name = "Ayrton Llanos" }
    });
    c.EnableAnnotations();
});

// 4. Dependency Injection (Shared)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// 5. Dependency Injection (Assignments)
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();
builder.Services.AddScoped<DataRecordRegisteredEventHandler>(); // Registrar el Event Handler

// 6. Dependency Injection (Tracking)
builder.Services.AddScoped<IDataRecordCommandService, DataRecordCommandService>();
builder.Services.AddScoped<IAssignmentsContextFacade, AssignmentsContextFacade>();

// 7. Mediator Configuration (Cortex)
builder.Services.AddMediator(options =>
{
    options.AddUnitOfWork<UnitOfWork>();
});

var app = builder.Build();

// 8. Crear Base de Datos Automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Esto crea las tablas y ejecuta el Seeding
}

// 9. HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();