using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices;
using MediatR;

namespace Crud.Devices.Api.Features.GetDevices;

public record GetDevicesQuery(string? Brand) : IRequest<List<Device>>;

public class GetDevicesHandler : IRequestHandler<GetDevicesQuery, List<Device>>
{
    private readonly IDevicesRepository _devices;

    public GetDevicesHandler(IDevicesRepository devices)
    {
        _devices = devices;
    }

    public Task<List<Device>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        return _devices.GetDevicesAsync(request.Brand, cancellationToken);
    }
}