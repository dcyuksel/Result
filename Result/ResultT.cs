using Result.Models;

namespace Result;

public sealed class Result<T> : Result
{
    public T? Payload { get; } = default;

    private Result(T payload) : base([], [])
    {
        Payload = payload;
    }

    private Result(List<Error> errors, List<Warning> warnings) : base(errors, warnings)
    {
    }

    public static Result<T> Ok(T payload)
    {
        return new Result<T>(payload);
    }

    public static new Result<T> Fail(params Error[] errors)
    {
        var result = new Result<T>([.. errors], []);

        return result;
    }

    public static implicit operator Result<T>(T payload)
    {
        return new Result<T>(payload);
    }
}
