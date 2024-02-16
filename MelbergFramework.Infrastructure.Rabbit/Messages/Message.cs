namespace MelbergFramework.Infrastructure.Rabbit.Messages;
public class Message
{
    public IDictionary<string, object> Headers { get; set; } = new Dictionary<string, object>();

    public byte[] Body { get; set; } = new byte[]{};
    public string RoutingKey { get; set; } = string.Empty;
     Should I change how I use Message?

     Easy answer is keep the standard message and message
}