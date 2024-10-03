using Result.Models;

namespace Result.Extensions;

public static class ResultExtensions
{
    public static void Merge(this Result result, params Result[] results)
    {
        var errors = results.SelectMany(r => r.Errors).ToList();
        result.AddRange(errors);

        var warnings = results.SelectMany(r => r.Warnings).ToList();
        result.AddRange(warnings);
    }

    public static void MergeErrorsAsWarnings(this Result result, params Result[] results)
    {
        var errors = results.SelectMany(r => r.Errors).ToList();
        result.AddRange(errors.Select(x => new Warning(x.Message)).ToList());

        var warnings = results.SelectMany(r => r.Warnings).ToList();
        result.AddRange(warnings);
    }

    public static Result<T> ToResult<T>(this Result result)
    {
        var resultT = Result<T>.Fail([.. result.Errors]);
        resultT.AddRange(result.Warnings);

        return resultT;
    }
}
