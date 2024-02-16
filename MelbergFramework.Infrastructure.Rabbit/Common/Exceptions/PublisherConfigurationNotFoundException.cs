namespace MelbergFramework.Infrastructure.Rabbit.Common.Exceptions;

public class PublisherConfigurationNotFoundException : Exception
{
    public PublisherConfigurationNotFoundException(string? message) : base(message) { }
}