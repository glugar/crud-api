using FluentValidation;

namespace Crud.Devices.Api.Features.UpdateDevice;

public class UpdateDeviceValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceValidator()
    {
        RuleFor(x => x.Brand)
            .NotNull()
            .WithMessage("Brand is required. All fields must be represented.");

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Name is required. All fields must be represented.");
        
        
    }
}