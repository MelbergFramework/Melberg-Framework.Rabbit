using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class BindingOptions
{
    [Required]
    public string Queue {get; set;}
    [Required]
    public string Connection {get; set;}
    [Required]
    public string Exchange {get; set;}
    [Required]
    public string SubscriptionKey {get; set;}
}