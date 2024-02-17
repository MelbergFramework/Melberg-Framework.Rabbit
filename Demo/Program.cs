using Demo.Processor;
using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace Demo;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        RabbitModule.RegisterMicroConsumer<DemoProcessor, TickMessage>(builder.Services, true);
        builder.Services.RegisterRequired();
        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.Configuration["Rabbit:ClientDeclarations:Connections:0:Password"] = app.Configuration["rabbit_pass"];
        } 
        app.Run();
    }
}
