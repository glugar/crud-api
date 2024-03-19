using Crud.Devices.Domain;

namespace Crud.Devices.Api.Models.Responses;

public class DevicesViewModel
{
    public List<Device> Devices { get; private set; }

    public DevicesViewModel(List<Device>? devices)
    {
        Devices = devices ?? [];
    }
}