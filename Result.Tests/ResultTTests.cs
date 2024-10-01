using FluentAssertions;
using Result.Models;

namespace Result.Tests;

public class ResultTTests
{
    private const string Message = "Message";

    [Fact]
    public void Ok()
    {
        var result = Result<string>.Ok(Message);
        result.Errors.Should().HaveCount(0);
        result.Warnings.Should().HaveCount(0);
        result.IsSuccessful.Should().BeTrue();
        result.IsFailed.Should().BeFalse();
        result.Payload.Should().Be(Message);
    }

    [Fact]
    public void Fail()
    {
        var result = Result<string>.Fail(new Error(Message));
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Message.Should().Be(Message);
        result.Warnings.Should().HaveCount(0);
        result.IsSuccessful.Should().BeFalse();
        result.IsFailed.Should().BeTrue();
        result.Payload.Should().BeNull();
    }
}
