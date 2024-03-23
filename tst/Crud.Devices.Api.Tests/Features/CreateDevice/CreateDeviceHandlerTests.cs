using Crud.Devices.Api.Features.CreateDevice;
using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace Crud.Devices.Api.Tests.Features.CreateDevice;

public class CreateDeviceHandlerTests
{

    private readonly IValidator<CreateDeviceCommand> _validator = Substitute.For<IValidator<CreateDeviceCommand>>();
    private readonly IDevicesRepository _devicesRepository = Substitute.For<IDevicesRepository>();
    
    [Fact]
    public async Task Handle_With_Valid_Command_Returns_Device()
    {
        // Arrange
        var request = new CreateDeviceCommand("name", "brand");
        
        _validator.Validate(request).Returns(new ValidationResult());

        
        _devicesRepository.CreateDeviceAsync(Arg.Any<Device>(), default).Returns(new Device());

        var handler = new CreateDeviceHandler(_validator, _devicesRepository);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.NotNull(result.AsT0);
        _validator.Received(1).Validate(request);
        await _devicesRepository.Received(1).CreateDeviceAsync(Arg.Any<Device>(), default);
    }

    [Fact]
    public async Task Handle_With_Invalid_Command_Returns_ValidationFailed()
    {
        // Arrange
        var request = new CreateDeviceCommand(string.Empty, "brand");

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Name", "Name must not be empty.")
        });

        _validator.Validate(request).Returns(validationResult);


        var handler = new CreateDeviceHandler(_validator, _devicesRepository);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        Assert.NotNull(result.AsT1);
        _validator.Received(1).Validate(request);
        await _devicesRepository.DidNotReceive().CreateDeviceAsync(Arg.Any<Device>(), default);
    }
}