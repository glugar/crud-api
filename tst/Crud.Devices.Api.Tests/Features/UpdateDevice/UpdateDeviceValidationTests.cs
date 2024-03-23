using Crud.Devices.Api.Features.UpdateDevice;
using FluentValidation.TestHelper;

namespace Crud.Devices.Api.Tests.Features.UpdateDevice;

public class UpdateDeviceValidationTests
{
    private readonly UpdateDeviceValidator _validator = new();

    [Fact]
    public void Id_Should_Not_Be_Empty()
    {
        // Arrange
        var model = new UpdateDeviceCommand(string.Empty, "Name", "Brand");
        
        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Fact]
    public void Name_Should_Not_Be_Empty()
    {
        // Arrange
        var model = new UpdateDeviceCommand("Id", string.Empty, "Brand");
        
        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Brand_Should_Not_Be_Empty()
    {
        // Arrange
        var model = new UpdateDeviceCommand("Id", "Name", string.Empty);
        
        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Brand);
    }

   
}