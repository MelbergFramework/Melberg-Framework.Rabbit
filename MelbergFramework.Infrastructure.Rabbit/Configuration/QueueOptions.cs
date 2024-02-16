using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class QueueOptions
{
    [Required]
    public string Name {get; set;}
    [Required]
    public string Connection {get; set;}
    [Required]
    public bool AutoDelete {get; set;}
    [Required]
    public bool Durable {get; set;}
    [Required]
    public bool Exclusive {get; set;}
}