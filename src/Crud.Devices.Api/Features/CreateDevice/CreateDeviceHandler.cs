using Crud.Devices.Api.Configuration;
using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices;
using FluentValidation;
using OneOf;
using MediatR;

namespace Crud.Devices.Api.Features.CreateDevice;

public record CreateDeviceCommand(string Name, string Brand) : IRequest<OneOf<Device, ValidationFailed>>;

public class CreateDeviceHandler : IRequestHandler<CreateDeviceCommand, OneOf<Device, ValidationFailed>>
{
    private readonly IValidator<CreateDeviceCommand> _validator;
    private readonly IDevicesRepository _devices;

    public CreateDeviceHandler(IValidator<CreateDeviceCommand> validator, IDevicesRepository devices)
    {
        _validator = validator;
        _devices = devices;
    }

    public async Task<OneOf<Device, ValidationFailed>> Handle(CreateDeviceCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            return new ValidationFailed(validationResult.Errors);


        return await _devices.CreateDeviceAsync(Device.Create(request.Name, request.Brand), cancellationToken)
            .ConfigureAwait(false);
    }
}