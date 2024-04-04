using Demo.Processor;
using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace Demo;

public class DemoRegistrator : Registrator
{
    public override void RegisterServices(IServiceCollection services)
    {
        RabbitModule.RegisterMicroConsumer<DemoProcessor, TickMessage>(services, false);
    }
}
