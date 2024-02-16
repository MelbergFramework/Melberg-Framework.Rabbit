using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class ServerDelcarationsOptions
{
    [Required]
    public List<ExchangeOptions> Exchanges {get; set;}
    public List<BindingOptions> Bindings {get; set;}
    public List<QueueOptions> Queues {get; set;}
}