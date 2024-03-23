using Crud.Devices.Api.Configuration;
using Crud.Devices.Api.Features.PatchDevice;
using Crud.Devices.Infrastructure.Devices;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using OneOf.Types;
using OneOf;

namespace Crud.Devices.Api.Tests.Features.PatchDevice;

public class PatchDeviceHandlerTests
{
    private readonly IValidator<PatchDeviceCommand> _validator = Substitute.For<IValidator<PatchDeviceCommand>>();
    private readonly IDevicesRepository _devicesRepository = Substitute.For<IDevicesRepository>();
    private readonly string _deviceId = "123";
    
    
    [Fact]
    public async Task Handle_With_Valid_Command_Returns_Success()
    {
        // Arrange
        var request = new PatchDeviceCommand(_deviceId, "Updated Device Name", "Updated Brand");
        
        _validator.Validate(request).Returns(new ValidationResult());
        _devicesRepository.UpdateDeviceAsync(_deviceId, request.Name, request.Brand, default).Returns(true);
        var handler = new PatchDeviceHandler(_devicesRepository, _validator);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.IsType<OneOf<Success, ValidationFailed, NotFound>>(result);
        Assert.IsType<Success>(result.Value);
        _validator.Received(1).Validate(request);
        await _devicesRepository.Received(1).UpdateDeviceAsync(_deviceId, request.Name, request.Brand, default);
    }

    [Fact]
    public async Task Handle_With_Invalid_Command_Returns_ValidationFailed()
    {
        // Arrange
        var request = new PatchDeviceCommand(_deviceId,"", "");
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name must not be empty."),
            new ValidationFailure("Brand", "Brand must not be empty.")
        });
        _validator.Validate(request).Returns(validationResult);
        var handler = new PatchDeviceHandler(_devicesRepository, _validator);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.IsType<OneOf<Success, ValidationFailed, NotFound>>(result);
        Assert.IsType<ValidationFailed>(result.Value);
        _validator.Received(1).Validate(request);
        await _devicesRepository.DidNotReceiveWithAnyArgs().UpdateDeviceAsync(_deviceId, "", "", default);
    }

    [Fact]
    public async Task Handle_With_NonExisting_Id_Returns_NotFound()
    {
        // Arrange
        var request = new PatchDeviceCommand(_deviceId,"Updated Device Name", "Updated Brand");
        
        _validator.Validate(request).Returns(new ValidationResult());
        _devicesRepository.UpdateDeviceAsync(_deviceId, request.Name, request.Brand, default).Returns(false);
        var handler = new PatchDeviceHandler(_devicesRepository, _validator);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.IsType<OneOf<Success, ValidationFailed, NotFound>>(result);
        Assert.IsType<NotFound>(result.Value);
        _validator.Received(1).Validate(request);
        await _devicesRepository.Received(1).UpdateDeviceAsync(_deviceId, request.Name, request.Brand, default);
    }
}
