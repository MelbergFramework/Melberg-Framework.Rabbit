using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace Demo.Processor;

public class DemoProcessor : IStandardConsumer
{
    private readonly ILogger<DemoProcessor> _logger;
    public DemoProcessor(ILogger<DemoProcessor> logger)
    {
        _logger = logger;
    }

    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {
        _logger.LogInformation("Read a message");
        
        await Task.Delay(1);
    }
}