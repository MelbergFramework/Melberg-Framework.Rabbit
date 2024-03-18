using Demo.Processor;
using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace Demo;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            
        var h =  MelbergHost
            .CreateHost()
            .DevelopmentPasswordReplacement(
                    "Rabbit:ClientDeclarations:Connections:0:Password",
                    "rabbit_pass")
            .AddServices(_ => 
            {
                RabbitModule.RegisterMicroConsumer<DemoProcessor, TickMessage>(_, false);
            })
            .AddControllers()
            .Build();
            await h.RunAsync();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
