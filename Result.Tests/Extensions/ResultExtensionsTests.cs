using FluentAssertions;
using Result.Extensions;
using Result.Models;

namespace Result.Tests.Extensions;

public class ResultExtensionsTests
{
    private const string Message = "Message";

    [Fact]
    public void MergeTwoOks()
    {
        var result1 = Result.Ok();
        var result2 = Result.Ok();
        result1.Merge(result2);

        result1.Errors.Should().HaveCount(0);
        result1.Warnings.Should().HaveCount(0);
        result1.IsSuccessful.Should().BeTrue();
        result1.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void MergeOneOkOneFail()
    {
        var result1 = Result.Ok();
        result1.Add(new Warning(Message)); 
        var result2 = Result.Fail(new Error(Message));
        result1.Merge(result2);

        result1.Warnings.Should().HaveCount(1);
        result1.Warnings.First().Message.Should().Be(Message);
        result1.Errors.Should().HaveCount(1);
        result1.Errors.First().Message.Should().Be(Message);
        result1.IsSuccessful.Should().BeFalse();
        result1.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void MergeTwoFails()
    {
        var result1 = Result.Ok();
        result1.Add(new Error(Message));
        result1.Add(new Warning(Message));
        var result2 = Result.Fail(new Error(Message));
        result1.Merge(result2);

        result1.Errors.Should().HaveCount(2);
        result1.Warnings.Should().HaveCount(1);
        result1.IsSuccessful.Should().BeFalse();
        result1.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void MergeErrorsAsWarnings()
    {
        var result1 = Result.Ok();
        var result2 = Result.Fail(new Error(Message));
        result1.MergeErrorsAsWarnings(result2);

        result1.Errors.Should().HaveCount(0);
        result1.Warnings.Should().HaveCount(1);
        result1.IsSuccessful.Should().BeTrue();
        result1.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void ToResultFromResult()
    {
        var result = Result.Fail(new Error(Message));
        result.Add(new Warning(Message));
        var resultT = result.ToResult<string>();

        resultT.Errors.Should().HaveCount(1);
        resultT.Warnings.Should().HaveCount(1);
        resultT.IsSuccessful.Should().BeFalse();
        resultT.IsFailed.Should().BeTrue();
    }
}
