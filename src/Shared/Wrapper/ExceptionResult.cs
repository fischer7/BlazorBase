using System.Collections.Generic;

namespace GlobalShared.Wrapper;
public sealed class ExceptionResult : Result
{
    public ExceptionResult(List<Error> errors)
        : base(false, IValidationResult.ValidationError) =>
        Errors = errors;

    public List<Error> Errors { get; }

    public static ExceptionResult WithErrors(List<Error> errors) => new(errors);
}