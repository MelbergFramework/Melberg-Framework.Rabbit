namespace MelbergFramework.Infrastructure.Rabbit.Common.Exceptions;

public class ConnectionConfigurationNotFoundException : Exception
{
    public ConnectionConfigurationNotFoundException(string? message) : base(message) { }
}