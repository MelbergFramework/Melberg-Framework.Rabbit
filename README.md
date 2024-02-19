# MelbergFramework.Infrastructure.Rabbit
RabbitMQ implementation for MelbergFramework


# Anatomy of Appsettings.json

The configuration for the MelbergFramework.Rabbit package resides in the Rabbit section of the appsettings.json file.

It is separated into two parts:

## Client Delcarations

### Connections

|Key|Description|
|-|-|
|Name|The value that will be used to relate this connection to the other sections.|
|ClientName|Mostly cosmetic, tells the Rabbit server who is connecting.|
|ServerName|The url of the rabbit server.|
|UserName|The username of the configured user.|
|Password|The password of the configured user.  Please see the Notes section on how to securely handle this for open source projects.|

### Async Recievers

|Key|Description|
|-|-|
|Name| The name of the model that the message will become on consumption.|
|Connection| The name of the connection to use to recieve messages.|
|Queue| The name of the queue that the consumer should bind to.|
|Scale| The number of consumers that should be created bound to that queue per instance of the application.|

### Publishers

|Key|Description|
|-|-|
|Name|The name of the model that will become a message.|
|Connection| The name of the connection to send messages with.|
|Exchange| The name of the exchange that the publisher will publish to.|

## Server Declarations

### Exchanges

|Key|Description|
|-|-|
|Name| The name of the topic.|
|Type| The type of exchange, currently supports Fanout, Direct, and Fanout.|
|AutoDelete| Declares whether the exchage deletes itself after losing all bindings.|
|Durable| Declares whether the exchange survives reboots of the rabbit server.|
|Connection| The name of the connection to create exchanges with.|

### Bindings

|Key|Description|
|-|-|
|Queue| The name of the queue that will be bound.|
|Connection| The name of the connection to bind with.|
|Exchange| The name of the exchange that will be bound.|
|SubscriptionKey| The binding that informs the Rabbit server which messages to send to the queue.|

### Queues

|Key|Description|
|-|-|
|Name| The name of the queue to create.|
|Connection| The name of the connection to use to create the queue.|
|AutoDelete| Declares whether the queue should survive losing its consumers.|
|Durable| Declares whether the queue should survive reboots of the rabbit server.|
|Exclusive| Declares whether the queue should be exclusive.|

# Notes

## General Execution

This project requires dotnet 6 sdk to run (install link [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)).

When running locally, I have the rabbit password replaced using the dotnet user-secrets tool. 
Please follow Microsoft's [guide](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux) to set the value of rabbit_pass to your configured rabbit account's password for the csproj file that you are executing.


# How to run a RabbitMQ Server

Please see Rabbit's documentation [here](https://hub.docker.com/_/rabbitmq).
