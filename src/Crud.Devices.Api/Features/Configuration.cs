using Crud.Devices.Api.Features.CreateDevice;
using Crud.Devices.Api.Features.DeleteDevice;
using Crud.Devices.Api.Features.GetDeviceById;
using Crud.Devices.Api.Features.GetDevices;
using Crud.Devices.Api.Features.PatchDevice;
using Crud.Devices.Api.Features.UpdateDevice;
using Crud.Devices.Infrastructure.Devices;

namespace Crud.Devices.Api.Features;


public static class Configuration
{
    public static IEndpointRouteBuilder MapDevicesEndpoints(this IEndpointRouteBuilder endpoints)
        => endpoints
            .MapCreateDevice()
            .MapGetDeviceById()
            .MapDeleteDeviceById()
            .MapGetDevices()
            .MapUpdateDevice()
            .MapPatchDevice();

    
    public static IServiceCollection AddDevicesFeature(
        this IServiceCollection services)
    {
        services
            .AddSingleton<IDevicesRepository, DevicesRepository>();
        return services;
    }
}