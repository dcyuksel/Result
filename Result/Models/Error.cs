namespace Result.Models;

public record Error(string Message) : ResultBaseModel(Message);
