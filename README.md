# Melberg-Framework.Rabbit
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
|Connection| Relates the consumer to a previously configured Connection.|
|Queue| The name of the queue that the consumer should bind to.|
|Scale| The number of consumers that should be created bound to that queue per instance of the application.|

### Publishers

|Key|Description|
|-|-|
|Name|The name of the model that will become a message.|
|Connection| Relates the publisher to a previously configured Connection.|
|Exchange| The name of the exchange that the publisher will publish to.|

## Server Declarations

### Exchanges

### Bindings

### Queues
