using FluentValidation;

namespace Crud.Devices.Api.Features.CreateDevice;

public class CreateDeviceValidation : AbstractValidator<CreateDeviceCommand>
{
    public CreateDeviceValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.Brand)
            .NotEmpty()
            .MaximumLength(50);
    }
}