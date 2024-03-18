using MelbergFramework.Core.HealthCheck;
using MelbergFramework.Infrastructure.Rabbit.Factories;
using RabbitMQ.Client;

namespace MelbergFramework.Infrastructure.Rabbit.Health;

public class RabbitPublisherHealthCheck<TMessage> : HealthCheck
{
    private readonly IConnection _connection;
    public RabbitPublisherHealthCheck(IStandardConnectionFactory factory)
    {
        _connection = factory.GetPublisherChannel(typeof(TMessage).Name);
    }
    public override string Name => "rabbitpublisher_"+typeof(TMessage).Name;

    public override Task<bool> IsOk(CancellationToken token) =>
        Task.FromResult(_connection.IsOpen);
}
