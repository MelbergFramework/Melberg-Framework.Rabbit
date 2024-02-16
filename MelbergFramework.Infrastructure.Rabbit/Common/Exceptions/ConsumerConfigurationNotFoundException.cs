namespace MelbergFramework.Infrastructure.Rabbit.Common.Exceptions;

public class ConsumerConfigurationNotFoundException : Exception
{
    public ConsumerConfigurationNotFoundException(string? message) : base(message) { }
}