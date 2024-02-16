using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class RabbitConfigurationOptions
{
    public static string Section => "Rabbit";
    
    [Required]
    public ClientDeclarationsOptions ClientDeclarations {get; set;}
    public ServerDelcarationsOptions ServerDelcarations {get; set;}
}