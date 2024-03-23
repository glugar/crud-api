using Crud.Devices.Api.Features.GetDeviceById;
using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices;
using NSubstitute;
using NotFound = OneOf.Types.NotFound;

namespace Crud.Devices.Api.Tests.Features.GetDeviceById;

public class GetDeviceByIdHandlerTests
{
    private readonly IDevicesRepository _devicesRepository = Substitute.For<IDevicesRepository>();
    [Fact]
    public async Task Handle_With_Existing_Id_Returns_Device()
    {
        // Arrange
        var deviceId = "123";
        var request = new GetDeviceByIdQuery( deviceId);
        var expectedDevice = Device.Fill(deviceId, "Test Device", "Test Brand", DateTime.Now);
        _devicesRepository.GetDeviceByIdAsync(deviceId, default).Returns(expectedDevice);
        var handler = new GetDeviceByIdHandler(_devicesRepository);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.NotNull(result.AsT0);
        Assert.Equal(expectedDevice, result.Value);
        await _devicesRepository.Received(1).GetDeviceByIdAsync(deviceId, default);
    }

    [Fact]
    public async Task Handle_With_NonExisting_Id_Returns_NotFound()
    {
        // Arrange
        var deviceId = "123";
        var request = new GetDeviceByIdQuery(deviceId);
        _devicesRepository.GetDeviceByIdAsync(deviceId, default).Returns((Device)null);
        var handler = new GetDeviceByIdHandler(_devicesRepository);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.IsType<NotFound>(result.Value);
        await _devicesRepository.Received(1).GetDeviceByIdAsync(deviceId, default);
    }
}