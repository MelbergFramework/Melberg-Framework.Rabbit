using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class PublisherOptions
{
    [Required]
    public string Name {get; set;}
    [Required]
    public string Connection {get; set;}
    [Required]
    public string Exchange {get; set;}
    public bool MaintainCorrelation {get; set;} = false;
}
