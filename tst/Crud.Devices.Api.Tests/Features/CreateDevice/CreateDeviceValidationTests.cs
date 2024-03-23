using Crud.Devices.Api.Features.CreateDevice;
using FluentValidation.TestHelper;

namespace Crud.Devices.Api.Tests.Features.CreateDevice;

public class CreateDeviceValidationTests
{
    private readonly CreateDeviceValidation _validator = new();

    [Fact]
    public void Name_Should_Not_Be_Empty()
    {
        // Arrange
        var model = new CreateDeviceCommand(string.Empty, "brand");
        
        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Name_Should_Not_Exceed_Maximum_Length()
    {
        // Arrange
        var model = new CreateDeviceCommand(new string('a', 51), "brand");

        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Brand_Should_Not_Be_Empty()
    {
        // Arrange
        var model = new CreateDeviceCommand("name", string.Empty);

        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Brand);
    }

    [Fact]
    public void Brand_Should_Not_Exceed_Maximum_Length()
    {
        // Arrange
        var model = new CreateDeviceCommand("Name", new string('a', 51));

        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Brand);
    }
}