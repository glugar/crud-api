using Crud.Devices.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Devices.Api.Features.GetDeviceById;

public static class Endpoint
{
    internal static IEndpointRouteBuilder MapGetDeviceById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/v1/device/{deviceId}", async (
                string deviceId,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {

                var result = await mediator.Send(new GetDeviceByIdQuery(deviceId), cancellationToken);

                return result.Match<IResult>(
                    Results.Ok,
                    _ => Results.NotFound()
                );
            })
            .WithTags("Devices")
            .Produces<Device>()
            .Produces(StatusCodes.Status404NotFound);
        
        return endpoints;
    }
}