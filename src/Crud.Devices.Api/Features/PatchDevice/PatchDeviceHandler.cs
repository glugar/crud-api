using Crud.Devices.Api.Configuration;
using Crud.Devices.Infrastructure.Devices;
using FluentValidation;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Crud.Devices.Api.Features.PatchDevice;

public record PatchDeviceCommand(string Id, string? Name, string? Brand) : IRequest<OneOf<Success, ValidationFailed, NotFound>>;

public class PatchDeviceHandler : IRequestHandler<PatchDeviceCommand, OneOf<Success, ValidationFailed, NotFound>>
{
    private readonly IDevicesRepository _devices;
    private readonly IValidator<PatchDeviceCommand> _validator;

    public PatchDeviceHandler(IDevicesRepository devices, IValidator<PatchDeviceCommand> validator)
    {
        _devices = devices;
        _validator = validator;
    }

    public async Task<OneOf<Success, ValidationFailed, NotFound>> Handle(PatchDeviceCommand request, CancellationToken cancellationToken)
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