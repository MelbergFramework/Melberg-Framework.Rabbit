using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Publishers;

namespace MelbergFramework.ComponentTesting.Rabbit;

public class MockPublisher <TMessage> : IStandardPublisher<TMessage>
    where TMessage : IStandardMessage
{
    public IList<TMessage> SentMessages = new List<TMessage>();

    public void Send(TMessage message)
    {
        SentMessages.Add(message);
    }
}
