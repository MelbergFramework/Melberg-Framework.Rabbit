using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class ConnectionOptions
{
    [Required]
    public string Name {get; set;}
    [Required]
    public string ClientName {get; set;}
    [Required]
    public string ServerName {get; set;}
    [Required]
    public string UserName {get; set;}
    [Required]
    public string Password {get; set;}
}