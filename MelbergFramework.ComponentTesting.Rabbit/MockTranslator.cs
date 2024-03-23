using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Translator;

namespace MelbergFramework.ComponentTesting.Rabbit;

public class MockTranslator<T> : IJsonToObjectTranslator<T>
{
    public List<T> Messages = new ();
    public int MessagePointer = 0;

    public T Translate(Message message)
    {
        if(MessagePointer < Messages.Count)
        {
            return Messages[MessagePointer++];
        }

        return default(T);
    }
}
