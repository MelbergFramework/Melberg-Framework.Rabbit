namespace MelbergFramework.Infrastructure.Rabbit.Messages;

public class Message
{
    public IDictionary<string, object> Headers { get; set; } = new Dictionary<string, object>();

    public byte[] Body { get; set; } = new byte[]{};
    public string RoutingKey { get; set; } = string.Empty;
}

public static class Headers
{
    public const string CorrelationId = "coid";
    public const string Timestamp = "timestamp";
}
