using System.Collections.Concurrent;
using MQTTnet;
using MQTTnet.Client;

namespace MQTTUI;

public class MqttService
{
    private readonly IMqttClient _mqttClient;
    private readonly ConcurrentBag<string> _messages;

    public MqttService()
    {
        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();
        _messages = new ConcurrentBag<string>();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .WithClientId("Client2_Subscriber")
            .WithWillQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();

        _mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            var message = $"Received Message on Topic: '{e.ApplicationMessage.Topic}' with Content '{e.ApplicationMessage.ConvertPayloadToString()}'";
            _messages.Add(message);
            await Task.CompletedTask;
        };

        _mqttClient.ConnectAsync(options, CancellationToken.None).Wait();
        SubscribeToTopics([
            "clear_tpm", "change_password", "seal_hash", "un_seal_hash", "delete_hash", "store_encryption_key",
            "get_encryption_key", "delete_encryption_key"
        ]).Wait();
    }

    private async Task SubscribeToTopics(List<string> topics)
    {
        foreach (var topic in topics)
        {
            var sub = await _mqttClient.SubscribeAsync(topic);
            // Handle subscription result if needed
        }
    }

    public IEnumerable<string> GetMessages()
    {
        return _messages;
    }
}