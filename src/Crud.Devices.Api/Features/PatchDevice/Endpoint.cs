using Crud.Devices.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Devices.Api.Features.PatchDevice;

public static class Endpoint
{
    internal static IEndpointRouteBuilder MapPatchDevice(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("/api/v1/device/{deviceId}", async (
                string deviceId,
                [FromBody] UpdateDeviceViewModel body,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {

                var result = await mediator.Send(new PatchDeviceCommand(deviceId, body.Name, body.Brand), cancellationToken);

                return result.Match<IResult>(
                    Results.Ok,
                    Results.BadRequest,
                    _ => Results.NotFound()
                );
            })
            .WithTags("Devices")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        return endpoints;
    }
}