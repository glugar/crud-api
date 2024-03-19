using Crud.Devices.Domain;

namespace Crud.Devices.Infrastructure.Devices;

public interface IDevicesRepository
{
    Task<Device> CreateDeviceAsync(Device device, CancellationToken cancellationToken);
    
    Task<Device?> GetDeviceByIdAsync(string id, CancellationToken cancellationToken);
    
    Task<List<Device>> GetDevicesAsync(string?  brand, CancellationToken cancellationToken);
    
    Task<bool> DeleteDeviceAsync(string id, CancellationToken cancellationToken);
    
    Task<bool> UpdateDeviceAsync(string id, string? name, string? brand, CancellationToken cancellationToken);
    
    Task<bool> PatchDeviceAsync(string id, string? name, string? brand, CancellationToken cancellationToken);
}