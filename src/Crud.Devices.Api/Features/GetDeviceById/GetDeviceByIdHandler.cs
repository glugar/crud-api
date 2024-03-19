using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices;
using OneOf;
using MediatR;
using OneOf.Types;

namespace Crud.Devices.Api.Features.GetDeviceById;

public record GetDeviceByIdQuery(string Id) : IRequest<OneOf<Device, NotFound>>;

public class GetDeviceByIdHandler : IRequestHandler<GetDeviceByIdQuery, OneOf<Device, NotFound>>
{
    private readonly IDevicesRepository _devices;

    public GetDeviceByIdHandler(IDevicesRepository devices)
    {
        _devices = devices;
    }

    public async Task<OneOf<Device, NotFound>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _devices.GetDeviceByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        return device is not null ? device : new NotFound();
    }
}