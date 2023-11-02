using GlobalShared.Constants.Erro;
using System;

namespace GlobalShared.Wrapper;

public class Result
{
    public Result()
    {
    }

    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; set; }

    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != ErrorConstants.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == ErrorConstants.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, ErrorConstants.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, ErrorConstants.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(ErrorConstants.NullValue);
}
