using Crud.Devices.Api.Configuration;
using Crud.Devices.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Devices.Api.Features.UpdateDevice;

public static class Endpoint
{
    internal static IEndpointRouteBuilder MapUpdateDevice(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/api/v1/device/{deviceId}", async (
                string deviceId,
                [FromBody] UpdateDeviceViewModel body,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {

                var result = await mediator.Send(new UpdateDeviceCommand(deviceId, body.Name, body.Brand), cancellationToken);

                return result.Match<IResult>(
                    Results.Ok,
                    Results.BadRequest,
                    _ => Results.NotFound()
                );
            })
            .WithTags("Devices")
            .Produces(StatusCodes.Status200OK)
            .Produces<ValidationFailed>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        
        return endpoints;
    }
}