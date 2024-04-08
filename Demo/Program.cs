using MelbergFramework.Application;

namespace Demo;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            
        var h =  MelbergHost
            .CreateHost<DemoRegistrator>()
            .DevelopmentPasswordReplacement(
                    "Rabbit:ClientDeclarations:Connections:0:Password",
                    "rabbit_pass")
            .Build();
            await h.RunAsync();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
