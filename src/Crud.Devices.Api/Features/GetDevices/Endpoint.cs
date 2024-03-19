using Crud.Devices.Api.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Devices.Api.Features.GetDevices;

public static class Endpoint
{
    internal static IEndpointRouteBuilder MapGetDevices(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/v1/device", async (
                [FromQuery]string? brand,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {

                var devices = await mediator.Send(new GetDevicesQuery(brand), cancellationToken).ConfigureAwait(false);
                return new DevicesViewModel(devices);
            })
            .WithTags("Devices")
            .Produces<DevicesViewModel>();
        
        return endpoints;
    }
}