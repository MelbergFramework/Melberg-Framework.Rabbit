using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class ExchangeOptions
{
    [Required]
    public string Name {get; set;}
    [Required]
    public string Type {get; set;}
    [Required]
    public bool AutoDelete {get; set;}
    [Required]
    public bool Durable {get; set;}
    [Required]
    public string Connection {get; set;}
}