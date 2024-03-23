using MelbergFramework.Application;
using MelbergFramework.Core.HealthCheck;
using MelbergFramework.Infrastructure.Rabbit.Configuration;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Factories;
using MelbergFramework.Infrastructure.Rabbit.Health;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Metrics;
using MelbergFramework.Infrastructure.Rabbit.Publishers;
using MelbergFramework.Infrastructure.Rabbit.Services;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MelbergFramework.Infrastructure.Rabbit;

public static class RabbitModule
{
    public static void RegisterMicroConsumer<TConsumer, TModel>(IServiceCollection catalog, bool sendMetrics)
    where TConsumer : class, IStandardConsumer
    where TModel : class
    {
        var selector = typeof(TModel).Name;
        catalog.AddSingleton<IStandardConnectionFactory, StandardConnectionFactory>();
        catalog.AddTransient<TConsumer,TConsumer>();
        catalog.AddSingleton<IHealthCheck>((s) => new RabbitConsumerHealthCheck(s,selector));
        catalog.AddTransient<IJsonToObjectTranslator<TModel>,JsonToObjectTranslator<TModel>>();
        RegisterConfiguration(catalog);
        if(sendMetrics)
        {
            RegisterMetrics(catalog);
        }
        catalog.AddHostedService(
            (s) => new RabbitMicroService<TConsumer>(
                selector,
                s,
                s.GetRequiredService<IOptions<RabbitConfigurationOptions>>(),
                s.GetRequiredService<IStandardConnectionFactory>(),
                sendMetrics ? s.GetRequiredService<IMetricPublisher>() : null,
                s.GetRequiredService<IOptions<ApplicationConfiguration>>(),
                s.GetRequiredService<ILogger<RabbitMicroService<TConsumer>>>())
        );
    }

    public static void RegisterMetrics(IServiceCollection catalog)
    {
        catalog.AddTransient<IMetricPublisher, MetricPublisher>();
        RegisterPublisher<MetricMessage>(catalog);
    }

    public static void RegisterPublisher<TMessage>(IServiceCollection catalog)
        where TMessage : IStandardMessage
    {
        catalog.AddSingleton<IStandardConnectionFactory, StandardConnectionFactory>();
        catalog.AddSingleton<IStandardPublisher<TMessage>,StandardPublisher<TMessage>>();
        catalog.AddSingleton<IHealthCheck,RabbitPublisherHealthCheck<TMessage>>();
        RegisterConfiguration(catalog);
    }
    
    private static void RegisterConfiguration(IServiceCollection catalog)
    {
        catalog.AddOptions<RabbitConfigurationOptions>()
            .BindConfiguration(RabbitConfigurationOptions.Section) 
            .ValidateDataAnnotations();
    }
}
