using Result.Models;

namespace Result;

public sealed class Result<T> : Result where T : notnull
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
        var validErrors = errors.Where(e => e is not null).ToList();
        if (validErrors.Count == 0)
        {
            validErrors.Add(new Error("Fail result is created without any error."));
        }

        return new Result<T>(validErrors, []);
    }

    public static implicit operator Result<T>(T payload)
    {
        return new Result<T>(payload);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Result<T>.Fail(error);
    }

    public static implicit operator Result<T>(Error[] errors)
    {
        return Result<T>.Fail(errors);
    }

    public static implicit operator Result<T>(List<Error> errors)
    {
        return Result<T>.Fail([.. errors]);
    }
}
