using FluentValidation;

namespace Crud.Devices.Api.Features.UpdateDevice;

public class UpdateDeviceValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required. All fields must be represented.");
        
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("Brand is required. All fields must be represented.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required. All fields must be represented.");
        
        
    }
}