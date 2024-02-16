using System.ComponentModel.DataAnnotations;

namespace MelbergFramework.Infrastructure.Rabbit.Configuration;

public class ClientDeclarationsOptions
{
    [Required]
    public List<ConnectionOptions> Connections {get; set;}
    public List<RecieverOptions> AsyncRecievers {get; set;}
    public List<PublisherOptions> Publishers {get; set;}
}