using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace MelbergFramework.Infrastructure.Rabbit.Consumers;

public interface IStandardConsumer
{
    Task ConsumeMessageAsync(Message message, CancellationToken ct);
}