using System.Diagnostics;
using MelbergFramework.Infrastructure.Rabbit.Configuration;
using MelbergFramework.Infrastructure.Rabbit.Extensions;
using MelbergFramework.Infrastructure.Rabbit.Factories;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MelbergFramework.Infrastructure.Rabbit.Publishers;

public abstract class BasePublisher<TMessage>
    where TMessage :  IStandardMessage
{
    private IModel _channel;
    protected IModel Channel 
    {
        get
        {
            if(_channel == null)
            {
                _channel = _connectionFactory.GetPublisherChannel(typeof(TMessage).Name).CreateModel();
            }
            return _channel;
        }
    }

    private readonly IStandardConnectionFactory _connectionFactory;
    private readonly PublisherOptions _config;
    private bool _disposed;


    public BasePublisher(IOptions<RabbitConfigurationOptions> configuration)
    {
        _config = RabbitConfigurator.GetPublisherOptions(typeof(TMessage).Name, configuration.Value);
        _connectionFactory = new StandardConnectionFactory(configuration);
    }


    public void Emit(Message message)
    {
        var properties = Channel.CreateBasicProperties();
        
        properties.Headers = message.Headers;
        
        properties.Headers.TryAdd(MessageExtensions.Headers.Timestamp,
                DateTime.UtcNow.ToString());

        properties.Headers[MessageExtensions.Headers.CorrelationId] = 
            _config.MaintainCorrelation ?
                Trace.CorrelationManager.ActivityId.ToString() : 
                Guid.NewGuid().ToString();

        Channel.BasicPublish(
            _config.Exchange,
            message.RoutingKey,
            true,
            properties,
            message.Body);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing && _channel != null)
        {
            _channel.Close();
        }

        _disposed = true;
    }
}
