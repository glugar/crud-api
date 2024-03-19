using Crud.Devices.Infrastructure.Devices;
using MediatR;
using OneOf.Types;
using OneOf;

namespace Crud.Devices.Api.Features.DeleteDevice;

public record DeleteDeviceCommand(string Id) : IRequest<OneOf<Success, NotFound>>;

public class DeleteDeviceHandler : IRequestHandler<DeleteDeviceCommand, OneOf<Success, NotFound>>
{
    private readonly IDevicesRepository _devices;

    public DeleteDeviceHandler(IDevicesRepository devices)
    {
        _devices = devices;
    }

    public async Task<OneOf<Success, NotFound>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _devices.DeleteDeviceAsync(request.Id, cancellationToken).ConfigureAwait(false);
        return deleted ? new Success() : new NotFound();
    }
}