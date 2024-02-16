using System.Collections.Concurrent;
using MelbergFramework.Infrastructure.Rabbit.Common.Exceptions;
using MelbergFramework.Infrastructure.Rabbit.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MelbergFramework.Infrastructure.Rabbit.Factories;

public interface IStandardConnectionFactory
{
    IModel GetConsumerModel(string name);
    IConnection GetPublisherChannel(string name); 
}

public class StandardConnectionFactory : IStandardConnectionFactory
{
    private static IModel _consumerChannel;
    private readonly RabbitConfigurationOptions _rabbitConfigurationOptions;

    private static ConcurrentDictionary<string,IConnection> _publisherConnections = new ConcurrentDictionary<string, IConnection>();
    public StandardConnectionFactory( IOptions<RabbitConfigurationOptions> options)
    {
        _rabbitConfigurationOptions = options.Value;
    }
    private IConnection GenerateConsumerConnection(string consumerName)
    {
        var receiverConfigs = _rabbitConfigurationOptions
                                .ClientDeclarations
                                .AsyncRecievers
                                .Where(_ => _.Name == consumerName);
        
        if(!receiverConfigs.Any())
            throw new ConsumerConfigurationNotFoundException($"Configuration was missing for Reciever with name {consumerName}");
        
        var connectionName = receiverConfigs.First().Connection;
        
        return MakeNewConnection(connectionName);
    }

    private IConnection GeneratePublisherChannel(string name)
    {
        
        var publisherOptions = _rabbitConfigurationOptions
                                .ClientDeclarations
                                .Publishers
                                .Where(_ => _.Name == name);
        
        if(!publisherOptions.Any())
            throw new PublisherConfigurationNotFoundException($"Configuration missing for Publisher {name}");
        
        var publisherConnection = publisherOptions.First().Connection;
        
        return MakeNewConnection(publisherConnection);
    }
    
    private IConnection MakeNewConnection(string connectionName)
    {
        var connectionConfigs = _rabbitConfigurationOptions
                                .ClientDeclarations
                                .Connections
                                .Where(_ => _.Name == connectionName);
        
        if(!connectionConfigs.Any())
            throw new ConnectionConfigurationNotFoundException($"Configuration missing for Connection with name {connectionName}");
        
        var connectionConfig = connectionConfigs.First();
        
        return MakeNewConnection(connectionConfig);
    }

    public IModel GetConsumerModel(string name = "IncommingMessages")
    {
        _consumerChannel??= GenerateConsumerConnection(name).CreateModel();
        
        return _consumerChannel;
    } 
    private IConnection MakeNewConnection(ConnectionOptions connectionConfig)
    {
        var factory = new ConnectionFactory()
        {
            UserName = connectionConfig.UserName,
            Password = connectionConfig.Password,
            VirtualHost = "/",
            DispatchConsumersAsync = true,
            HostName = connectionConfig.ServerName,
            ClientProvidedName = connectionConfig.ClientName,
        };
        return factory.CreateConnection();
    }

    public IConnection GetPublisherChannel(string name)
    {
        return _publisherConnections.GetOrAdd(name, GeneratePublisherChannel(name));
    }
} 