using MelbergFramework.Infrastructure.Rabbit.Configuration;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.Options;

namespace MelbergFramework.Infrastructure.Rabbit.Publishers;

public interface IStandardPublisher<TMessage>
    where TMessage : IStandardMessage
{
    void Send(TMessage message);
}
public class StandardPublisher<T> : BasePublisher<T>, IStandardPublisher<T> where T : IStandardMessage
{
    private readonly IObjectToJsonTranslator _translator = new ObjectToJsonTranslator();

    public StandardPublisher(IOptions<RabbitConfigurationOptions> configurationProvider) : base(configurationProvider) { }
    public virtual void Send(T message)
    {
        var result = _translator.Translate(message);
        Emit(result);
    }
}