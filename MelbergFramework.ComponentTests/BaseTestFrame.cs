using LightBDD.MsTest3;
using MelbergFramework.Infrastructure.Rabbit.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MelbergFramework.ComponentTests;
public class BaseTestFrame : FeatureFixture
{
    public WebApplication App;

    public T GetClass<T>() => (T)App
        .Services
        .GetRequiredService(typeof(T));
    
    public RabbitMicroService<TestProcessor> GetService() =>
        (RabbitMicroService<TestProcessor>)App
            .Services
            .GetServices<IHostedService>()
            .Where(_ => 
                    _.GetType() == typeof(RabbitMicroService<TestProcessor>))
            .First();
}
