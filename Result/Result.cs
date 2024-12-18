﻿using Result.Models;

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

    public static Result Fail(params List<Error> errors)
    {
        var validErrors = errors.Where(e => e is not null).ToList();
        if (validErrors.Count == 0)
        {
            validErrors.Add(new Error("Fail result is created without any error."));
        }

        return new Result(validErrors, []);
    }

    public static implicit operator Result(Error error)
    {
        return Fail(error);
    }

    public static implicit operator Result(List<Error> errors)
    {
        return Fail(errors);
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