using Result.Models;

namespace Result;

public class Result
{
    public bool IsSuccessful => Errors.Count == 0;
    public bool IsFailed => !IsSuccessful;

    public List<Error> Errors { get; protected set; } = [];
    public List<Warning> Warnings { get; protected set; } = [];

    protected Result(List<Error> errors, List<Warning> warnings)
    {
        Errors = errors;
        Warnings = warnings;
    }

    public static Result Ok()
    {
        return new Result([], []);
    }

    public static Result Fail(params Error[] errors)
    {
        var result = new Result([.. errors], []);

        return result;
    }

    public void Add(ResultBaseModel model)
    {
        if (model is Error)
        {
            Errors.Add((model as Error)!);

            return;
        }

        if (model is Warning)
        {
            Warnings.Add((model as Warning)!);

            return;
        }
    }

    public void AddRange(IReadOnlyList<ResultBaseModel> models)
    {
        foreach (var model in models)
        {
            Add(model);
        }
    }
}