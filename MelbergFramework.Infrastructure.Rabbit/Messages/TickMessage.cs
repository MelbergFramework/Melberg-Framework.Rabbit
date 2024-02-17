namespace MelbergFramework.Infrastructure.Rabbit.Messages;

public class TickMessage : StandardMessage
{
    public override string GetRoutingKey() => "";//I don't like how this implementation works
}