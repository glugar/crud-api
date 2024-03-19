using Crud.Devices.Api.Configuration;
using Crud.Devices.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Devices.Api.Features.CreateDevice;


public static class Endpoint
{
    internal static IEndpointRouteBuilder MapCreateDevice(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/v1/device", async (
                CreateDeviceCommand request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {

                var result = await mediator.Send(request, cancellationToken);

                return result.Match<IResult>(
                    d => Results.Created($"/api/v1/device/{d.Id}", d),
                    validationFailed => Results.BadRequest(validationFailed.Errors)
                );
            })
            .WithTags("Devices")
            .Produces<Device>(StatusCodes.Status201Created)
            .Produces<ValidationFailed>(StatusCodes.Status400BadRequest);
        
        return endpoints;
    }
}