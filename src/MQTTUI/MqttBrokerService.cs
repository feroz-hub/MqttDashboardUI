namespace MQTTUI;

public class MqttBrokerService
{

    public static async Task ServerConnection()
    {
        var mqttFactory = new MqttFactory();
        var options = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint().Build();
        List<string> allowedClientIds = ["Client1", "Client2", "Test"];
        using var mqttServer = mqttFactory.CreateMqttServer(options);
        mqttServer.ValidatingConnectionAsync += e =>
        {
            if (!allowedClientIds.Contains(e.ClientId))
            {
                e.ReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid;

            }
            return Task.CompletedTask;
            ;
        };
        await mqttServer.StartAsync();
        await mqttServer.StopAsync();
    }
    
}