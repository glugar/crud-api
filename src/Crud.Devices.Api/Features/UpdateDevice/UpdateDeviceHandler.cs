using Crud.Devices.Api.Configuration;
using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices;
using FluentValidation;
using OneOf;
using MediatR;
using OneOf.Types;

namespace Crud.Devices.Api.Features.UpdateDevice;

public record UpdateDeviceCommand(string Id, string Name, string Brand)
    : IRequest<OneOf<Success, ValidationFailed, NotFound>>;

public class UpdateDeviceHandler : IRequestHandler<UpdateDeviceCommand, OneOf<Success, ValidationFailed, NotFound>>
{
    private readonly IValidator<UpdateDeviceCommand> _validator;
    private readonly IDevicesRepository _devices;

    public UpdateDeviceHandler(IValidator<UpdateDeviceCommand> validator, IDevicesRepository devices)
    {
        _validator = validator;
        _devices = devices;
    }

    public async Task<OneOf<Success, ValidationFailed, NotFound>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {
            return new ValidationFailed(result.Errors);
        }
        
        var updated = await _devices.UpdateDeviceAsync(request.Id, request.Name, request.Brand, cancellationToken).ConfigureAwait(false);
        return updated ? new Success() : new NotFound();
        
        
    }
}