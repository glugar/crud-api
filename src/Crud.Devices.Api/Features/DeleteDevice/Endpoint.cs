using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Devices.Api.Features.DeleteDevice;

public static class Endpoint
{
    internal static IEndpointRouteBuilder MapDeleteDeviceById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/api/v1/device/{deviceId}", async (
                string deviceId,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {

                var result = await mediator.Send(new DeleteDeviceCommand(deviceId), cancellationToken);

                return result.Match<IResult>(
                    Results.Ok,
                    _ => Results.NotFound()
                );
            })
            .WithTags("Devices")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        return endpoints;
    }
}