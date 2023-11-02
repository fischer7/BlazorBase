using System.Collections.Generic;

namespace GlobalShared.Wrapper;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "ValidationError",
        "A validation problem occurred.");

    List<Error> Errors { get; }
}
