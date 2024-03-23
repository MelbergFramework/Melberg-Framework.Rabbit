using MelbergFramework.Application;
using MelbergFramework.Core.Time;
using MelbergFramework.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MelbergFramework.Infrastructure.Rabbit.Services;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using MelbergFramework.ComponentTesting.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Publishers;

namespace MelbergFramework.ComponentTests;

public partial class BaseTest : BaseTestFrame
{
    public BaseTest()
    {
        App = MelbergHost
            .CreateHost<TestRegistrator>()
            .AddServices( (_) => 
                {

                })
            .Build();
    }

    public async Task Consume_message()
    {
        var consumer = GetClass<RabbitMicroService<TestProcessor>>();
        await consumer.ConsumeMessageAsync(null,CancellationToken.None);
    }

    public async Task Setup_message(int value)
    {
        var mockTranslator = 
            (MockTranslator<InboundMessage>)
            GetClass<IJsonToObjectTranslator<InboundMessage>>();

        mockTranslator.Messages.Add(new InboundMessage(){ Value = value});
    }

    public async Task Verify_value(int value)
    {
        var pub = (MockPublisher<OutboundMessage>)GetClass<IStandardPublisher<OutboundMessage>>();
        Assert.IsTrue(pub.SentMessages.Count == 1);
        Assert.IsTrue(pub.SentMessages.First().Value == value);
    }

}

public class TestRegistrator : Registrator
{
    public override void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ISmallTest,SmallTest>();
        ClockModule.RegisterClock(services);
    }
}

public interface ISmallTest
{
    DateTime GetTime();
}
public class SmallTest : ISmallTest
{
    private IClock _clock;
    public SmallTest(IClock clock)
    {
        _clock = clock;
    }

    public DateTime GetTime() =>
        _clock.GetUtcNow();
}
