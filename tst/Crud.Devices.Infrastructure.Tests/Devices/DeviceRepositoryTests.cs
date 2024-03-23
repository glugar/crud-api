using Crud.Devices.Domain;

namespace Crud.Devices.Infrastructure.Tests.Devices;

public class DeviceRepositoryTests : IDisposable
{
    private readonly RepoSetup _repo;
    public DeviceRepositoryTests()
    {
        _repo = ReposHolder.Instance;
    }

    public void Dispose()
    {
        _repo.Dispose();
    }


    private Device? DefaultDevice = Device.Fill("123", "Name", "Brand", DateTime.Now);
    
    private Device GetNewDevice()
        => Device.Create( "Name", "Brand");
    
    private async Task RefreshDevice()
    {
        DefaultDevice = await _repo.DevicesRepository.GetDeviceByIdAsync(DefaultDevice!.Id, CancellationToken.None).ConfigureAwait(false);
    }
    private async Task PrepareDb()
    {
        Device device = await _repo.DevicesRepository.CreateDeviceAsync(GetNewDevice(), CancellationToken.None).ConfigureAwait(false);
        DefaultDevice = device;
    }
    
    
    [Fact]
    public async Task DeleteDevice()
    {
        await PrepareDb().ConfigureAwait(false);
        
        var deviceInDb = await _repo.DevicesRepository.GetDeviceByIdAsync(DefaultDevice!.Id, CancellationToken.None).ConfigureAwait(false);
        Assert.NotNull(deviceInDb);
        
        await _repo.DevicesRepository.DeleteDeviceAsync(DefaultDevice.Id, CancellationToken.None).ConfigureAwait(false);

        await RefreshDevice().ConfigureAwait(false);
        Assert.Null(DefaultDevice);
    }

    [Fact]
    public async Task CreateDevice()
    {
        await _repo.DevicesRepository.CreateDeviceAsync(GetNewDevice(), CancellationToken.None);
        
        var devices = await _repo.DevicesRepository.GetDevicesAsync(null, CancellationToken.None);
        Assert.Single(devices);
        
        await _repo.DevicesRepository.DeleteDeviceAsync(devices.First().Id, CancellationToken.None);
    }
    
    [Fact]
    public async Task GetDeviceById()
    {
        await PrepareDb().ConfigureAwait(false);
        
        var device = await _repo.DevicesRepository.GetDeviceByIdAsync(DefaultDevice!.Id, CancellationToken.None).ConfigureAwait(false);
        Assert.NotNull(device);
        Assert.Equal(DefaultDevice.Id, device.Id);
        
        await _repo.DevicesRepository.DeleteDeviceAsync(DefaultDevice.Id, CancellationToken.None);
    }
    
    [Fact]
    public async Task GetDevicesByBrand()
    {
        await PrepareDb().ConfigureAwait(false);
        
        var devices = await _repo.DevicesRepository.GetDevicesAsync("bRand", CancellationToken.None).ConfigureAwait(false);
        Assert.Single(devices);
        
        await _repo.DevicesRepository.DeleteDeviceAsync(devices.First().Id, CancellationToken.None);
    }

    [Fact]
    public async Task UpdateDevice()
    {
        await PrepareDb().ConfigureAwait(false);
        await _repo.DevicesRepository.UpdateDeviceAsync(DefaultDevice!.Id, "NewName", "NewBrand", CancellationToken.None).ConfigureAwait(false);
        
        await RefreshDevice().ConfigureAwait(false);
        
        Assert.Equal("NewName", DefaultDevice!.Name);
        Assert.Equal("NewBrand", DefaultDevice!.Brand);
        await _repo.DevicesRepository.DeleteDeviceAsync(DefaultDevice.Id, CancellationToken.None);
    }

    [Fact]
    public async Task PatchDevice()
    {
        await PrepareDb().ConfigureAwait(false);
        
        await _repo.DevicesRepository.UpdateDeviceAsync(DefaultDevice!.Id, "NewName", null, CancellationToken.None).ConfigureAwait(false);
        await RefreshDevice().ConfigureAwait(false);
        
        Assert.Equal("NewName", DefaultDevice!.Name);
        Assert.Equal("Brand", DefaultDevice!.Brand);
        await _repo.DevicesRepository.DeleteDeviceAsync(DefaultDevice.Id, CancellationToken.None);
    }
}