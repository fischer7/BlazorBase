namespace GlobalShared.Wrapper;

public class Result<TValue> : Result, IResult<TValue>
{
    public Result() { }
    public TValue? Value { get; set; }

    public Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        Value = value;

    //public TValue Value => IsSuccess
    //    ? _value!
    //    : throw new InvalidOperationException("The value of a failure result can not be accessed.");
    //public TValue? Value => _value;

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}

public interface IResult
{

}
public interface IResult<out T> : IResult
{
    T? Value { get; }
}