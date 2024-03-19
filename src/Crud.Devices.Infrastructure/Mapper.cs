using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices.Models;

namespace Crud.Devices.Infrastructure;

public static class Mapper
{
    public static DeviceEntity MapToDeviceEntity(this Device device)
        => new()
        {
            Id = device.Id,
            Brand = device.Brand,
            Name = device.Name,
            CreationDate = device.CreationDate
        };
}