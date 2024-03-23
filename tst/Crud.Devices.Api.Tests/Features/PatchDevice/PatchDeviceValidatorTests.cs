using Crud.Devices.Api.Features.PatchDevice;
using FluentValidation.TestHelper;

namespace Crud.Devices.Api.Tests.Features.PatchDevice;

public class PatchDeviceValidatorTests
{
    private readonly PatchDeviceValidator _validator = new();
    
    
    [Fact]
    public void Id_Should_Not_Be_Empty()
    {
        // Arrange
        var model = new PatchDeviceCommand(string.Empty, "Name", "Brand");
        
        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    [Fact]
    public void At_Least_One_Of_The_Fields_Must_Be_Present()
    {
        // Arrange
        var model = new PatchDeviceCommand("Id", null, null);
        
        var result = _validator.TestValidate(model);
        
        // Act & Assert
        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("At least one of the fields must be present.");
    }
}