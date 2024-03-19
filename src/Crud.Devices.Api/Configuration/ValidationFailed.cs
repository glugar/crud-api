using FluentValidation.Results;

namespace Crud.Devices.Api.Configuration;

public class ValidationFailed
{
    public IEnumerable<ValidationFailure> Errors { get; }

    public ValidationFailed(IEnumerable<ValidationFailure> errors)
    {
        Errors = errors;
    }
}