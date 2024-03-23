using FluentValidation;

namespace Crud.Devices.Api.Features.PatchDevice;

public class PatchDeviceValidator : AbstractValidator<PatchDeviceCommand>
{
    public PatchDeviceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
        
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.Brand))
            .WithMessage("At least one of the fields must be present.");
    }
}