using MelbergFramework.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using MelbergFramework.ComponentTesting.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Publishers;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Extensions;

namespace MelbergFramework.ComponentTests;

public partial class BaseTest : BaseTestFrame
{
    private Guid _coID = Guid.NewGuid();
    public BaseTest()
    {
        App = MelbergHost
            .CreateHost<TestRegistrator>()
            .AddServices( (_) => 
                {
                    _.OverridePublisher<OutboundMessage>();
                    _.OverrideTranslator<InboundMessage>();
                    _.PrepareConsumer<TestProcessor>();
                })
            .Build();
    }

    public async Task Consume_message()
    {
        var consumer = GetService();
        var message = new Message();
        message.SetCoID(_coID);
        await consumer.ConsumeMessageAsync(message,CancellationToken.None);
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
        Assert.IsTrue(pub.SentMessages.First().Test == _coID);
    }

}

public class TestRegistrator : Registrator
{
    public override void RegisterServices(IServiceCollection services)
    {
        RabbitModule.RegisterMicroConsumer<TestProcessor,InboundMessage>
            (services,true);
        RabbitModule.RegisterPublisher<OutboundMessage>(services);
    }
}

