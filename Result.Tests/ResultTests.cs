using FluentAssertions;
using Result.Models;

namespace Result.Tests;

public class ResultTests
{
    private const string Message = "Message";

    [Fact]
    public void Ok()
    {
        var result = Result.Ok();
        result.Errors.Should().HaveCount(0);
        result.Warnings.Should().HaveCount(0);
        result.IsSuccessful.Should().BeTrue();
        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void Fail()
    {
        var result = Result.Fail(new Error(Message));
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Message.Should().Be(Message);
        result.Warnings.Should().HaveCount(0);
        result.IsSuccessful.Should().BeFalse();
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void FailWithEmptyErrorsShouldAddDefaultError()
    {
        var result = Result.Fail();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Message.Should().Be("Fail result is created without any error.");
        result.Warnings.Should().HaveCount(0);
        result.IsSuccessful.Should().BeFalse();
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Add()
    {
        var result = Result.Ok();
        result.Add(new Warning(Message));
        result.Errors.Should().HaveCount(0);
        result.Warnings.Should().HaveCount(1);
        result.Warnings.First().Message.Should().Be(Message);
        result.IsSuccessful.Should().BeTrue();
        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void AddRange()
    {
        var result = Result.Ok();
        var errors = new List<Error> { new(Message), new(Message), new(Message) };
        var warnings = new List<Warning> { new(Message), new(Message) };
        result.AddRange(errors);
        result.AddRange(warnings);
        result.Errors.Should().HaveCount(3);
        result.Warnings.Should().HaveCount(2);
        result.IsSuccessful.Should().BeFalse();
        result.IsFailed.Should().BeTrue();
    }
}
