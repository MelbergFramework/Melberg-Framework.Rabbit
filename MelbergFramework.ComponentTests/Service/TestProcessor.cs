using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Publishers;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.Logging;

namespace MelbergFramework.ComponentTests;

public class TestProcessor : IStandardConsumer
{
    private readonly ILogger<TestProcessor> _logger;
    private readonly IJsonToObjectTranslator<InboundMessage> _translator;
    private readonly IStandardPublisher<OutboundMessage> _publisher;

    public TestProcessor(
        IJsonToObjectTranslator<InboundMessage> translator,
        IStandardPublisher<OutboundMessage> publisher,
        ILogger<TestProcessor> logger)
    {
        _publisher = publisher;
        _logger = logger;
        _translator = translator;
    }

    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {

        var translatedMessage = _translator.Translate(message);
        _publisher.Send(new OutboundMessage(){Value = translatedMessage.Value});
        
        await Task.Delay(1);
    }
}

public class InboundMessage : StandardMessage
{
    public int Value {get; set;}

    public override string GetRoutingKey() => "a";
}

public class OutboundMessage : StandardMessage
{
    public int Value {get; set;}
    public override string GetRoutingKey() => "a";
}
