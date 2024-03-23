using Crud.Devices.Api.Features.DeleteDevice;
using Crud.Devices.Infrastructure.Devices;
using NSubstitute;
using OneOf.Types;
using OneOf;

namespace Crud.Devices.Api.Tests.Features.DeleteDevice;

public class DeleteDeviceHandlerTests
{
    private readonly IDevicesRepository _devicesRepository = Substitute.For<IDevicesRepository>();
    [Fact]
    public async Task Handle_With_Existing_Id_Returns_Success()
    {
        // Arrange
        var deviceId = "123";
        var request = new DeleteDeviceCommand(deviceId); 
        _devicesRepository.DeleteDeviceAsync(deviceId, default).Returns(true);
        var handler = new DeleteDeviceHandler(_devicesRepository);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.IsType<OneOf<Success, NotFound>>(result);
        Assert.IsType<Success>(result.Value);
        await _devicesRepository.Received(1).DeleteDeviceAsync(deviceId, default);
    }

    [Fact]
    public async Task Handle_With_NonExisting_Id_Returns_NotFound()
    {
        // Arrange
        var deviceId = "123";
        var request = new DeleteDeviceCommand(deviceId);
        _devicesRepository.DeleteDeviceAsync(deviceId, default).Returns(false);
        var handler = new DeleteDeviceHandler(_devicesRepository);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.IsType<OneOf<Success, NotFound>>(result);
        Assert.IsType<NotFound>(result.Value);
        await _devicesRepository.Received(1).DeleteDeviceAsync(deviceId, default);
    }
}